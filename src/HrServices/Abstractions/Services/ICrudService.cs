using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrServices.DTOs;
using HrServices.DTOs.Filters;

namespace HrServices.Abstractions.Services
{
    public interface ICrudService<TEntity, TCreateDto, TUpdateDto>
    {
        // todo: paged get
        public Task<TEntity> CreateAsync(TEntity model);
        public Task<TEntity> CreateAsync(TCreateDto dto);
        public Task<TEntity> UpdateAsync(TEntity model);
        public Task<TEntity> UpdateAsync(Guid id, TUpdateDto dto);
        public Task<TEntity> DeleteAsync(Guid Id);
        public Task<TEntity> GetByIdAsync(Guid id);
        public Task<Page<TEntity>> GetPagedAsync(PageFilters pageFilters);
        public Task<ICollection<TEntity>> GetFilteredAsync(Func<TEntity, bool> predicate);
        public Task<Page<TEntity>> GetFilteredPageAsync(Func<TEntity, bool> predicate, PageFilters pageFilters);
    }
}
