using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrServices.DTOs;

namespace HrServices.Abstractions.Services
{
    public interface ICrudService<TEntity>
    {
        // todo: paged get
        public Task<TEntity> CreateAsync(TEntity model);
        public Task<TEntity> UpdateAsync(Guid id, TEntity model);
        public Task<TEntity> DeleteAsync(Guid Id);
        public Task<TEntity> GetByIdAsync(Guid id);
        public Task<Page<TEntity>> GetPagedAsync();
        public Task<TEntity> GetFilteredAsync(Func<TEntity, bool> predicate);
    }
}
