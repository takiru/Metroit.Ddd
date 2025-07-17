using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metroit.DDD.Infrastructure.EFCore
{
    /// <summary>
    /// Entity Framework Coreのリポジトリの基本操作を提供します。
    /// </summary>
    /// <typeparam name="T">エンティティクラス。</typeparam>
    public abstract class EFRepositoryBase<T> where T : class
    {
        /// <summary>
        /// 現在利用しているコンテキストを取得します。
        /// </summary>
        protected DbContext DbContext { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="dbContext"><see cref="DbContext"/> オブジェクト。</param>
        public EFRepositoryBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// すべてのレコードを取得します。
        /// </summary>
        /// <returns><typeparamref name="T"/> レコードコレクション。</returns>
        public List<T> GetAll()
        {
            return DbContext.Set<T>()
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// すべてのレコードを取得します。
        /// </summary>
        /// <returns><typeparamref name="T"/> レコードコレクション。</returns>
        public Task<List<T>> GetAllAsyc()
        {
            return DbContext.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// エンティティを追加します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public int Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// エンティティを追加します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> AddAsync(T entity)
        {
            DbContext.Set<T>().Add(entity);
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// エンティティのコレクションを追加します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int AddRange(params T[] entities)
        {
            DbContext.Set<T>().AddRange(entities);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// エンティティのコレクションを追加します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> AddRangeAsync(params T[] entities)
        {
            DbContext.Set<T>().AddRange(entities);
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// エンティティのコレクションを追加します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int AddRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// エンティティのコレクションを追加します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            DbContext.Set<T>().AddRange(entities);
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// エンティティを更新します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public int Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// エンティティを更新します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> UpdateAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// エンティティのコレクションを更新します。
        /// </summary>
        /// <param name="entity">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int UpdateRange(params T[] entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// エンティティのコレクションを更新します。
        /// </summary>
        /// <param name="entity">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> UpdateRangeAsync(params T[] entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// エンティティのコレクションを更新します。
        /// </summary>
        /// <param name="entity">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int UpdateRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// エンティティのコレクションを更新します。
        /// </summary>
        /// <param name="entity">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> UpdateRangeAsync(IEnumerable<T> entities)
        {
            DbContext.Set<T>().UpdateRange(entities);
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// エンティティを削除します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public int Remove(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// エンティティを削除します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> RemoveAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// エンティティのコレクションを削除します。
        /// </summary>
        /// <param name="entity">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int RemoveRange(params T[] entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// エンティティのコレクションを削除します。
        /// </summary>
        /// <param name="entity">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> RemoveRangeAsync(params T[] entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// エンティティのコレクションを削除します。
        /// </summary>
        /// <param name="entity">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int RemoveRange(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// エンティティのコレクションを削除します。
        /// </summary>
        /// <param name="entity">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> RemoveRangeAsync(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            return DbContext.SaveChangesAsync();
        }
    }
}
