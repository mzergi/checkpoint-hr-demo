using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs;
using HrServices.Entities;

namespace HrServices.Services
{
    // todo: add automapper
    public class CrudService<TEntity> : ICrudService<TEntity> where TEntity : BaseEntity
    {
        private IBaseRepository<TEntity> Repository { get; set; }
        public CrudService(IBaseRepository<TEntity> repository) {
            Repository = repository;
        }
        public async Task<TEntity> CreateAsync(TEntity model)
        {
            return await Repository.AddAsync(model);
        }

        public async Task<TEntity> DeleteAsync(Guid id)
        {
            return await Repository.DeleteByIdAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Repository.GetByIdAsync(id) ?? throw new InvalidOperationException("Entity does not exist.");
        }

        public Task<Page<TEntity>> GetPagedAsync()
        {
            // todo paged
            throw new NotImplementedException();
        }

        public Task<TEntity> GetFilteredAsync(Func<TEntity, bool> predicate)
        {
            // todo paged
            throw new NotImplementedException();
        }

        public async Task<TEntity> UpdateAsync(Guid id, TEntity model)
        {
            model.Id = id;
            return await Repository.UpdateEntityAsync(model);
        }
    }
}
