using HrServices.Abstractions.Repositories;
using HrServices.DTOs.Filters;
using HrServices.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrDataAccess.Repositories
{
    // todo: Tenant handling
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected PostgresHrDbContext Context;
        public BaseRepository(PostgresHrDbContext context)
        {
            this.Context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<T>> AddRangeAsync(ICollection<T> entities)
        {
            Context.Set<T>().AddRange(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        public async Task HardDeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await Context.Set<T>().ForEachAsync(e => e.IsDeleted = true);
        }

        public async Task<T> DeleteByIdAsync(Guid id)
        {
            var entity = await Context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            entity.IsDeleted = true;
            await Context.SaveChangesAsync();
            return entity;

        }

        public async Task<ICollection<T>> GetAllAsync(bool isDeleted = false)
        {
            return await Context.Set<T>().Where(s => s.IsDeleted == isDeleted).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, bool isDeleted = false)
        {
            return await Context.Set<T>().Where(s => s.Id == id && s.IsDeleted == isDeleted).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetQuery(Func<T, bool> predicate, bool isDeleted = false)
        {
            // todo: QueryProvider
            // this could be improved by extracting to a private function but I couldnt figure it out
            bool FinalPredicate(T s) => predicate(s) && s.IsDeleted == isDeleted;
            
            return Context.Set<T>().Where((Func<T, bool>)FinalPredicate).AsQueryable();
        }

        public IQueryable<T> GetQuery(Func<T, bool> predicate, PageFilters pageFilters, bool isDeleted = false)
        {
            // todo: QueryProvider

            return GetQuery(predicate, isDeleted)
                .Skip(pageFilters.PageSize * pageFilters.CurrentPage)
                .Take(pageFilters.PageSize);
        }

        public async Task<ICollection<T>> GetQueriedListAsync(Func<T, bool> predicate, bool isDeleted = false)
        {
            // todo: QueryProvider
            // todo: this throws an exception as you can't call async
            // todo: removed async for now
            return GetQuery(predicate, isDeleted).ToList();
        }

        public async Task<ICollection<T>> GetQueriedListAsync(Func<T, bool> predicate, PageFilters pageFilters, bool isDeleted = false)
        {
            // todo: QueryProvider
            // todo: this throws an exception as you can't call async
            // todo: removed async for now
            return GetQuery(predicate, pageFilters, isDeleted).ToList();
        }

        public async Task<ICollection<T>> UpdateEntitiesAsync(ICollection<T> entities)
        {
            Context.Set<T>().UpdateRange(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

        public async Task<T> UpdateEntityAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
    }
}
