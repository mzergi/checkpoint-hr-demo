using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs;
using HrServices.DTOs.Filters;
using HrServices.Entities;

namespace HrServices.Services
{
    // todo: add automapper
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
            return await GetFilteredPageAsync(_ => true, pageFilters);
        }

        public async Task<ICollection<TEntity>> GetFilteredAsync(Func<TEntity, bool> predicate)
        {
            return await Repository.GetQueriedListAsync(predicate);
        }

        public async Task<Page<TEntity>> GetFilteredPageAsync(Func<TEntity, bool> predicate, PageFilters pageFilters)
        {
            var result = Mapper.Map<PageFilters, Page<TEntity>>(pageFilters);
            result.Entries = await Repository.GetQueriedListAsync(predicate, pageFilters);
            result.TotalPages = (result.Entries.Count + pageFilters.PageSize - 1) / pageFilters.PageSize;
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
