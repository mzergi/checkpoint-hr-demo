using System.Linq.Expressions;
using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs;
using HrServices.DTOs.Filters;
using HrServices.Entities;

namespace HrServices.Services
{
    public class CrudService<TEntity, TCreateDto, TUpdateDto> : ICrudService<TEntity, TCreateDto, TUpdateDto> where TEntity : BaseEntity
    {
        private IBaseRepository<TEntity> Repository { get; set; }
        private IMapper Mapper { get; set; }
        
        public CrudService(IBaseRepository<TEntity> repository, IMapper mapper) {
            Repository = repository;
            Mapper = mapper;
        }
        
        public async Task<TEntity> CreateAsync(TEntity model)
        {
            return await Repository.AddAsync(model);
        }

        public async Task<TEntity> CreateAsync(TCreateDto dto)
        {
            var model = Mapper.Map<TEntity>(dto);
            return await CreateAsync(model);
        }

        public async Task<TEntity> DeleteAsync(Guid id)
        {
            return await Repository.DeleteByIdAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Repository.GetByIdAsync(id) ?? throw new InvalidOperationException("Entity does not exist.");
        }

        public async Task<Page<TEntity>> GetPagedAsync(PageFilters pageFilters)
        {
            return await GetFilteredPageAsync(pageFilters: pageFilters);
        }

        public async Task<ICollection<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            PageFilters? pageFilters = null)
        {
            return await Repository.GetQueriedListAsync(filter, orderBy, pageFilters);
        }

        public async Task<Page<TEntity>> GetFilteredPageAsync(PageFilters pageFilters, 
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            var result = Mapper.Map<PageFilters, Page<TEntity>>(pageFilters);
            result.Entries = await Repository.GetQueriedListAsync(filter, orderBy, pageFilters);
            result.TotalItems = await Repository.CountAsync();
            result.TotalPages = (result.TotalItems + result.PageSize - 1) / result.PageSize;
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity model)
        {
            return await Repository.UpdateEntityAsync(model);
        }

        public async Task<TEntity> UpdateAsync(Guid id, TUpdateDto dto)
        {
            var entity = await GetByIdAsync(id);
            var model = Mapper.Map(dto, entity);
            return await UpdateAsync(model);
        }
    }
}
