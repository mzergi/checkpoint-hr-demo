using HrServices.DTOs.Filters;
using HrServices.Entities;

namespace HrServices.Abstractions.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<ICollection<T>> AddRangeAsync(ICollection<T> entities);
        Task HardDeleteAsync(T entity);
        Task DeleteAllAsync();
        Task<T> DeleteByIdAsync(Guid id);
        Task<ICollection<T>> GetAllAsync(bool isDeleted = false);
        Task<T?> GetByIdAsync(Guid id, bool isDeleted = false);
        IQueryable<T> GetQuery(Func<T, bool> predicate, bool isDeleted = false);
        IQueryable<T> GetQuery(Func<T, bool> predicate, PageFilters pageFilters, bool isDeleted = false);
        Task<ICollection<T>> GetQueriedListAsync(Func<T, bool> predicate, bool isDeleted = false);
        Task<ICollection<T>> GetQueriedListAsync(Func<T, bool> predicate, PageFilters pageFilters, bool isDeleted = false);
        Task<ICollection<T>> UpdateEntitiesAsync(ICollection<T> entities);
        Task<T> UpdateEntityAsync(T entity);

        // todo: more functionality
    }
}
