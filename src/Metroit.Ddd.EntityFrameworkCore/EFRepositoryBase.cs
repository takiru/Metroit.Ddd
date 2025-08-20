using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Metroit.Ddd.EntityFrameworkCore
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
        /// 追加、更新、削除を行った直後に、変更追跡をクリアするかどうかを示します。
        /// </summary>
        protected bool InstantlyClearChangeTracker { get; set; } = false;

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
        /// <param name="cancellationToken"> キャンセルトークン。</param>
        /// <returns><typeparamref name="T"/> レコードコレクション。</returns>
        public Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return DbContext.Set<T>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// エンティティを追加します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public int Add(T entity)
        {
            ExecutingAdd(entity);
            DbContext.Set<T>().Add(entity);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティを追加します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> AddAsync(T entity)
        {
            ExecutingAdd(entity);
            DbContext.Set<T>().Add(entity);
            var result = DbContext.SaveChangesAsync();
            result.ContinueWith(t =>
            {
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    InstructClearChangeTracker();
                }
            }, TaskScheduler.Default);
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを追加します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int AddRange(params T[] entities)
        {
            foreach (var entity in entities)
            {
                ExecutingAdd(entity);
            }
            DbContext.Set<T>().AddRange(entities);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを追加します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public Task<int> AddRangeAsync(params T[] entities)
        {
            foreach (var entity in entities)
            {
                ExecutingAdd(entity);
            }
            DbContext.Set<T>().AddRange(entities);
            var result = DbContext.SaveChangesAsync();
            result.ContinueWith(t =>
            {
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    InstructClearChangeTracker();
                }
            }, TaskScheduler.Default);
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを追加します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int AddRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                ExecutingAdd(entity);
            }
            DbContext.Set<T>().AddRange(entities);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを追加します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> AddRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                ExecutingAdd(entity);
            }
            DbContext.Set<T>().AddRange(entities);
            var result = await DbContext.SaveChangesAsync();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティを更新します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public int Update(T entity)
        {
            ExecutingUpdate(entity);
            DbContext.Set<T>().Update(entity);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティを更新します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> UpdateAsync(T entity)
        {
            ExecutingUpdate(entity);
            DbContext.Set<T>().Update(entity);
            var result = await DbContext.SaveChangesAsync();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを更新します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int UpdateRange(params T[] entities)
        {
            foreach (var entity in entities)
            {
                ExecutingUpdate(entity);
            }
            DbContext.Set<T>().UpdateRange(entities);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを更新します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> UpdateRangeAsync(params T[] entities)
        {
            foreach (var entity in entities)
            {
                ExecutingUpdate(entity);
            }
            DbContext.Set<T>().UpdateRange(entities);
            var result = await DbContext.SaveChangesAsync();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを更新します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                ExecutingUpdate(entity);
            }
            DbContext.Set<T>().UpdateRange(entities);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを更新します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> UpdateRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                ExecutingUpdate(entity);
            }
            DbContext.Set<T>().UpdateRange(entities);
            var result = await DbContext.SaveChangesAsync();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティを削除します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public int Remove(T entity)
        {
            ExecutingRemove(entity);
            DbContext.Set<T>().Remove(entity);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティを削除します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> RemoveAsync(T entity)
        {
            ExecutingRemove(entity);
            DbContext.Set<T>().Remove(entity);
            var result = await DbContext.SaveChangesAsync();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを削除します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int RemoveRange(params T[] entities)
        {
            foreach (var entity in entities)
            {
                ExecutingRemove(entity);
            }
            DbContext.Set<T>().RemoveRange(entities);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを削除します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> RemoveRangeAsync(params T[] entities)
        {
            foreach (var entity in entities)
            {
                ExecutingRemove(entity);
            }
            DbContext.Set<T>().RemoveRange(entities);
            var result = await DbContext.SaveChangesAsync();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを削除します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                ExecutingRemove(entity);
            }
            DbContext.Set<T>().RemoveRange(entities);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを削除します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> RemoveRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                ExecutingRemove(entity);
            }
            DbContext.Set<T>().RemoveRange(entities);
            var result = await DbContext.SaveChangesAsync();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// 変更追跡をクリアします。
        /// </summary>
        public void ClearChangeTracker()
        {
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            DbContext.ChangeTracker.Clear();
#else
            DbContext.ChangeTracker.Entries().ToList()
                .ForEach(x => x.State = EntityState.Detached);
#endif
        }

        /// <summary>
        /// 変更追跡の即時クリアを行う。
        /// </summary>
        private void InstructClearChangeTracker()
        {
            if (InstantlyClearChangeTracker)
            {
                ClearChangeTracker();
            }
        }

        /// <summary>
        /// 追加を行うときに呼び出されます。
        /// </summary>
        /// <param name="entity">追加を行うエンティティ。</param>
        protected virtual void ExecutingAdd(T entity) { }

        /// <summary>
        /// 更新を行うときに呼び出されます。
        /// </summary>
        /// <param name="entity">更新を行うエンティティ。</param>
        protected virtual void ExecutingUpdate(T entity) { }

        /// <summary>
        /// 削除を行うときに呼び出されます。
        /// </summary>
        /// <param name="entity">削除を行うエンティティ。</param>
        protected virtual void ExecutingRemove(T entity) { }
    }
}
