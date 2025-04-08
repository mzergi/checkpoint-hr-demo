using System.Linq.Expressions;
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
        // todo: CreatedAt is uninitialized
        // todo: UpdatedAt is uninitialized
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

        public IQueryable<T> GetQuery(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            PageFilters? pageFilters = null,
            bool isDeleted = false)
        {
            IQueryable<T> query = Context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (pageFilters != null)
            {
                query = query.Skip(pageFilters.PageSize * pageFilters.CurrentPage)
                    .Take(pageFilters.PageSize);
            }

            query = query.Where(s => s.IsDeleted == isDeleted);

            return orderBy != null ? orderBy(query) : query;
        }

        public async Task<ICollection<T>> GetQueriedListAsync(Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            PageFilters? pageFilters = null,
            bool isDeleted = false)
        {
            return await GetQuery(filter, orderBy, pageFilters, isDeleted).ToListAsync();
        }
        // todo: CreatedAt is uninitialized
        // todo: UpdatedAt is uninitialized
        public async Task<ICollection<T>> UpdateEntitiesAsync(ICollection<T> entities)
        {
            Context.Set<T>().UpdateRange(entities);
            await Context.SaveChangesAsync();
            return entities;
        }
        // todo: CreatedAt is uninitialized
        // todo: UpdatedAt is uninitialized
        public async Task<T> UpdateEntityAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync()
        {
            return await Context.Set<T>().Where(s => !s.IsDeleted).CountAsync();
        }
    }
}
