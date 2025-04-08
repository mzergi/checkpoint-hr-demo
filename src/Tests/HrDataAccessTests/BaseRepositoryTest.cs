using HrDataAccess;
using HrDataAccess.Repositories;
using HrServices.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrDataAccessTests
{
    // todo: error cases
    public abstract class BaseRepositoryTest<TRepository, TEntity> where TEntity : BaseEntity where TRepository : BaseRepository<TEntity>
    {
        private readonly PostgresHrDbContext dbContext;
        private readonly TRepository repository;
        protected readonly BogusHelper bogusHelper;
        public BaseRepositoryTest() {
            var builder = new DbContextOptionsBuilder<PostgresHrDbContext>();
            builder.UseInMemoryDatabase(databaseName: "CheckPointHr");

            var dbContextOptions = builder.Options;
            dbContext = new PostgresHrDbContext(dbContextOptions);
            dbContext.Database.EnsureCreated();

            repository = (TRepository)new BaseRepository<TEntity>(dbContext);
            bogusHelper = new BogusHelper();
        }

        [Fact]
        public async Task CrudRepository_CreateOneItem_ShouldCreateEntity()
        {
            // setup
            CreateFake(out List<TEntity> entities, 1);
            var entity = entities.First();

            // act
            var created = await repository.AddAsync(entity);
            var result = await repository.GetByIdAsync(created.Id);

            // assert
            Assert.Equal(created, result);
        }

        [Fact]
        public async Task CrudRepository_CreateManyItems_ShouldCreateEntities()
        {
            // setup
            CreateFake(out List<TEntity> entities, 100);

            // act
            var created = await repository.AddRangeAsync(entities);
            var createdIds = created.Select(c => c.Id).ToList();

            var result = repository.GetQuery(s => createdIds.Contains(s.Id)).ToList();

            // assert
            Assert.Equal(created.Count, result.Count);
            Assert.Equal(100, result.Count);
            Assert.Equal(created, result);
        }

        [Fact]
        public async Task CrudRepository_HardDelete_ShouldHardDeleteEntity()
        {
            // setup
            CreateFake(out List<TEntity> entities, 1);
            var entity = entities.First();

            // act
            var created = await repository.AddAsync(entity);
            await repository.HardDeleteAsync(created);
            var softDeleteresult = await repository.GetByIdAsync(created.Id, true);
            var result = await repository.GetByIdAsync(created.Id, false);

            // assert
            Assert.NotNull(created);
            Assert.Null(softDeleteresult);
            Assert.Null(result);
        }

        [Fact]
        public async Task CrudRepository_DeleteAllItems_ShouldDeleteAllItems()
        {
            // setup
            CreateFake(out List<TEntity> entities, 10);

            // act
            var created = await repository.AddRangeAsync(entities);
            var createdIds = created.Select(c => c.Id).ToList();

            await repository.DeleteAllAsync();

            var result = repository.GetQuery(s => createdIds.Contains(s.Id)).ToList();
            var softDeleteResult = repository.GetQuery(s => createdIds.Contains(s.Id), isDeleted: true).ToList();

            // assert
            Assert.Equal(10, created.Count);
            Assert.Empty(result);
            Assert.Equal(created.Count, softDeleteResult.Count);
        }

        [Fact]
        public async Task CrudRepository_DeleteItem_ShouldDeleteItem()
        {
            // setup
            CreateFake(out List<TEntity> entities, 10);
            var entity = entities.First();

            // act
            var created = await repository.AddAsync(entity);

            await repository.DeleteByIdAsync(created.Id);

            var result = await repository.GetByIdAsync(created.Id);
            var softDeleteResult = await repository.GetByIdAsync(created.Id, true);

            // assert
            Assert.NotNull(created);
            Assert.Null(result);
            Assert.NotNull(softDeleteResult);
        }

        [Fact]
        public async Task CrudRepository_GetByIdAsync_ShouldReturnCorrectEntity()
        {
            // setup
            CreateFake(out List<TEntity> entities, 10);
            var entity = entities.First();

            // act
            var created = await repository.AddAsync(entity);

            var result = await repository.GetByIdAsync(created.Id);
            var softDeleteResult = await repository.GetByIdAsync(created.Id, true);

            // assert
            Assert.NotNull(created);
            Assert.Equal(created, result);
            Assert.Null(softDeleteResult);
        }

        [Fact]
        public async Task CrudRepository_GetQuery_ShouldReturnQueriedEntities()
        {
            // setup
            CreateFake(out List<TEntity> entities, 100);
            var tenantId = Guid.NewGuid();
            entities.ForEach(entity => entity.TenantId = tenantId);
            // act
            var created = await repository.AddRangeAsync(entities);
            var createdIds = created.Select(c => c.Id).ToList();

            var result = repository.GetQuery(s => createdIds.Contains(s.Id) && s.TenantId == tenantId).ToList();

            // assert
            Assert.Equal(created.Count, result.Count);
            Assert.Equal(100, result.Count);
            Assert.Equal(created, result);
        }

        [Fact]
        public async Task CrudRepository_UpdateEntities_ShouldUpdateEntities()
        {
            // setup
            CreateFake(out List<TEntity> entities, 100);
            // act
            var created = (await repository.AddRangeAsync(entities)).ToList();
            var createdIds = created.Select(c => c.Id).ToList();

            var tenantId = Guid.NewGuid();
            created.ForEach(s => s.TenantId = tenantId);
            var updated = await repository.UpdateEntitiesAsync(created);

            var result = repository.GetQuery(s => createdIds.Contains(s.Id) && s.TenantId == tenantId).ToList();

            // assert
            Assert.Equal(created.Count, result.Count);
            Assert.Equal(100, result.Count);
            Assert.Equal(updated, result);
        }

        [Fact]
        public async Task CrudRepository_UpdateEntityAsync_ShouldUpdateEntity()
        {
            // setup
            CreateFake(out List<TEntity> entities, 1);
            var entity = entities.First();
            // act
            var created = await repository.AddAsync(entity);

            var tenantId = Guid.NewGuid();
            created.TenantId = tenantId;
            var updated = await repository.UpdateEntityAsync(created);

            var result = repository.GetQuery(s => s.Id == created.Id && s.TenantId == tenantId).ToList().First();

            // assert
            Assert.NotNull(created);
            Assert.Equal(updated, result);
        }

        public abstract void CreateFake(out List<TEntity> result, int number = 1);
    }
}
