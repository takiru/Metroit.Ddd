using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metroit.DDD.Infrastructure.EF
{
    /// <summary>
    /// Entity Framework Coreのリポジトリの基本操作を提供します。
    /// </summary>
    /// <typeparam name="T">エンティティクラス。</typeparam>
    public abstract class EFRepositoryBase<T> where T : class
    {
        protected readonly DbContext DbContext;

        public EFRepositoryBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public int Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            return DbContext.SaveChanges();
        }

        public Task<int> AddAsync(T entity)
        {
            DbContext.Set<T>().Add(entity);
            return DbContext.SaveChangesAsync();
        }

        public int AddRange(T[] entities)
        {
            DbContext.Set<T>().AddRange(entities);
            return DbContext.SaveChanges();
        }

        public Task<int> AddRangeAsync(T[] entities)
        {
            DbContext.Set<T>().AddRange(entities);
            return DbContext.SaveChangesAsync();
        }

        public int AddRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
            return DbContext.SaveChanges();
        }

        public Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
            return DbContext.SaveChangesAsync();
        }

        public int Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
            return DbContext.SaveChanges();
        }

        public Task<int> UpdateAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            return DbContext.SaveChangesAsync();
        }

        public int UpdateRange(T[] entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
            return DbContext.SaveChanges();
        }

        public Task<int> UpdateRangeAsync(T[] entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
            return DbContext.SaveChangesAsync();
        }

        public int UpdateRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
            return DbContext.SaveChanges();
        }

        public Task<int> UpdateRangeAsync(IEnumerable<T> entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
            return DbContext.SaveChangesAsync();
        }

        public int Remove(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            return DbContext.SaveChanges();
        }

        public Task<int> RemoveAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            return DbContext.SaveChangesAsync();
        }

        public int RemoveRange(T[] entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChanges();
        }

        public Task<int> RemoveRangeAsync(T[] entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChangesAsync();
        }

        public int RemoveRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChanges();
        }
        public Task<int> RemoveRangeAsync(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChangesAsync();
        }
    }
}
