using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrServices.Abstractions.Services
{
    public interface ICrudService<TCreateModel, TUpdateModel, TReturnModel, TEntity>
    {
        // todo: paged get
        public Task<TReturnModel> CreateAsync(TCreateModel model);
        public Task<TReturnModel> UpdateAsync(TUpdateModel model);
        public Task<TReturnModel> DeleteAsync(TEntity entity);
        public Task<TReturnModel> GetByIdAsync(Guid id);
        public Task<TReturnModel> GetFilteredAsync(Func<TEntity, bool> predicate);
    }
}
