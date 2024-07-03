using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.Entities;

namespace HrServices.Services
{
    // todo: add automapper
    public class CrudService<TCreateModel, TUpdateModel, TReturnModel, TEntity> : ICrudService<TCreateModel, TUpdateModel, TReturnModel, TEntity> where TEntity : BaseEntity
    {
        private IBaseRepository<TEntity> repository { get; set; }
        public CrudService(IBaseRepository<TEntity> _repository) {
            repository = _repository;
        }
        public async Task<TReturnModel> CreateAsync(TCreateModel model)
        {
            throw new NotImplementedException();
        }

        public Task<TReturnModel> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TReturnModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TReturnModel> GetFilteredAsync(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TReturnModel> UpdateAsync(TUpdateModel model)
        {
            throw new NotImplementedException();
        }
    }
}
