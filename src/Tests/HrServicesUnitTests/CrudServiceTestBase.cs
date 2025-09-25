using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.DTOs.Filters;
using HrServices.Entities;
using HrServices.Services;
using Moq;

namespace HrServicesUnitTests;

// TEntity must be concrete entity, TCreateDto/TUpdateDto are its DTOs,
// TRepository is the repository interface used by the concrete service,
// TService is the concrete service which extends CrudService<TEntity,...>.
public abstract class CrudServiceTestBase<TEntity, TCreateDto, TUpdateDto, TRepository, TService>
    where TEntity : BaseEntity, new()
    where TRepository : class, IBaseRepository<TEntity>
    where TService : CrudService<TEntity, TCreateDto, TUpdateDto>
{
    protected readonly Mock<TRepository> RepoMock = new();
    protected readonly IMapper Mapper;
    protected readonly TService Service;

    protected CrudServiceTestBase(Action<IMapperConfigurationExpression> addMaps, Func<TRepository, IMapper, TService> serviceFactory)
    {
        var cfg = new MapperConfiguration(c =>
        {
            // Page profile mapping is required by CrudService.GetFilteredPageAsync
            c.AddProfile(new HrServices.AutoMapper.PageProfile());
            addMaps(c);
        });
        Mapper = cfg.CreateMapper();
        Service = serviceFactory(RepoMock.Object, Mapper);
    }

    // Common CRUD tests

    [Fact]
    public async Task Crud_Create_WithDto_MapsAndAdds()
    {
        RepoMock.Setup(r => r.AddAsync(It.IsAny<TEntity>())).ReturnsAsync((TEntity e) => e);
        var dto = CreateDtoFactory();

        var created = await Service.CreateAsync(dto);

        AssertOnMappedCreate(dto, created);
        RepoMock.Verify(r => r.AddAsync(It.IsAny<TEntity>()), Times.Once);
    }

    [Fact]
    public async Task Crud_Create_WithEntity_CallsAdd()
    {
        var entity = CreateEntityFactory();
        RepoMock.Setup(r => r.AddAsync(entity)).ReturnsAsync(entity);

        var res = await Service.CreateAsync(entity);

        Assert.Equal(entity.Id, res.Id);
        RepoMock.Verify(r => r.AddAsync(entity), Times.Once);
    }

    [Fact]
    public async Task Crud_GetById_Returns()
    {
        var id = Guid.NewGuid();
        var entity = CreateEntityFactory();
        entity.Id = id;
        RepoMock.Setup(r => r.GetByIdAsync(id, false)).ReturnsAsync(entity);

        var res = await Service.GetByIdAsync(id);

        Assert.Equal(id, res.Id);
    }

    [Fact]
    public async Task Crud_GetById_Throws_WhenMissing()
    {
        var id = Guid.NewGuid();
        RepoMock.Setup(r => r.GetByIdAsync(id, false)).ReturnsAsync((TEntity?)null);

        await Assert.ThrowsAsync<InvalidOperationException>(() => Service.GetByIdAsync(id));
    }

    [Fact]
    public async Task Crud_GetByIds_Filters()
    {
        var ids = new[] { Guid.NewGuid(), Guid.NewGuid() };
        var items = new List<TEntity> { new() { Id = ids[0] }, new() { Id = ids[1] } };
        RepoMock.Setup(r => r.GetQueriedListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<TEntity, bool>>>(), null, null, false))
                .ReturnsAsync(items);

        var res = await Service.GetByIdsAsync(ids);

        Assert.Equal(2, res.Count);
    }

    [Fact]
    public async Task Crud_GetFiltered_Delegates()
    {
        RepoMock.Setup(r => r.GetQueriedListAsync(null, null, null, false)).ReturnsAsync(new List<TEntity> { new() });

        var res = await Service.GetFilteredAsync();

        Assert.Single(res);
    }

    [Fact]
    public async Task Crud_GetFilteredPage_ComputesTotals()
    {
        var filters = new PageFilters { CurrentPage = 2, PageSize = 10 };
        RepoMock.Setup(r => r.GetQueriedListAsync(null, null, filters, false)).ReturnsAsync(new List<TEntity> { new(), new() });
        RepoMock.Setup(r => r.CountAsync()).ReturnsAsync(23);

        var page = await Service.GetFilteredPageAsync(filters);

        Assert.Equal(3, page.TotalPages);
        Assert.Equal(23, page.TotalItems);
        Assert.Equal(2, page.Entries.Count);
        Assert.Equal(filters.PageSize, page.PageSize);
        Assert.Equal(filters.CurrentPage, page.CurrentPage);
    }

    [Fact]
    public async Task Crud_Update_WithEntity_CallsRepo()
    {
        var entity = CreateEntityFactory();
        RepoMock.Setup(r => r.UpdateEntityAsync(entity)).ReturnsAsync(entity);

        var res = await Service.UpdateAsync(entity);

        Assert.Equal(entity.Id, res.Id);
        RepoMock.Verify(r => r.UpdateEntityAsync(entity), Times.Once);
    }

    [Fact]
    public async Task Crud_Update_WithIdAndDto_LoadsAndMaps()
    {
        var id = Guid.NewGuid();
        var entity = CreateEntityFactory();
        entity.Id = id;
        RepoMock.Setup(r => r.GetByIdAsync(id, false)).ReturnsAsync(entity);
        RepoMock.Setup(r => r.UpdateEntityAsync(entity)).ReturnsAsync(entity);
        var dto = UpdateDtoFactory();

        var res = await Service.UpdateAsync(id, dto);

        AssertOnUpdated(entity, res);
    }

    [Fact]
    public async Task Crud_Delete_CallsRepo()
    {
        var id = Guid.NewGuid();
        var entity = CreateEntityFactory();
        entity.Id = id;
        RepoMock.Setup(r => r.DeleteByIdAsync(id)).ReturnsAsync(entity);

        var res = await Service.DeleteAsync(id);

        Assert.Equal(id, res.Id);
        RepoMock.Verify(r => r.DeleteByIdAsync(id), Times.Once);
    }

    // Factories and assertions to be provided by concrete tests

    protected abstract TCreateDto CreateDtoFactory();
    protected abstract TUpdateDto UpdateDtoFactory();
    protected abstract TEntity CreateEntityFactory();
    protected abstract void AssertOnMappedCreate(TCreateDto dto, TEntity created);
    protected abstract void AssertOnUpdated(TEntity beforeUpdated, TEntity updated);
}
