using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HrServices.DTOs;
using HrServices.DTOs.Filters;

namespace HrServices.Abstractions.Services
{
    public interface ICrudService<TEntity, TCreateDto, TUpdateDto>
    {
        public Task<TEntity> CreateAsync(TEntity model);
        public Task<TEntity> CreateAsync(TCreateDto dto);
        public Task<TEntity> UpdateAsync(TEntity model);
        public Task<TEntity> UpdateAsync(Guid id, TUpdateDto dto);
        public Task<TEntity> DeleteAsync(Guid Id);
        public Task<TEntity> GetByIdAsync(Guid id);
        public Task<Page<TEntity>> GetPagedAsync(PageFilters pageFilters);
        public Task<ICollection<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            PageFilters? pageFilters = null);
        public Task<Page<TEntity>> GetFilteredPageAsync(PageFilters pageFilters, 
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    }
}
