using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Metroit.Ddd.EntityFrameworkCore
{
    /// <summary>
    /// Entity Framework Coreのリポジトリの基本操作を提供します。
    /// </summary>
    /// <typeparam name="T1">エンティティクラス。</typeparam>
    /// <typeparam name="T2">利用する <see cref="DbContext"/> クラス。</typeparam>
    public abstract class EFRepositoryBase<T1, T2> where T1 : class where T2 : DbContext
    {
        /// <summary>
        /// 現在利用しているコンテキストを取得します。
        /// </summary>
        protected T2 DbContext { get; }

        /// <summary>
        /// 用意された命令で検索、追加、更新、削除を行ったとき、変更追跡をするかどうかを取得します。
        /// </summary>
        protected bool AllwaysNoTracking { get; set; } = false;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="dbContext"><see cref="DbContext"/> オブジェクト。</param>
        public EFRepositoryBase(T2 dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// 主キーを条件としたレコードを取得します。
        /// </summary>
        /// <param name="ifThrowNoDataException">レコードが存在しなかったときにスローするかどうか。</param>
        /// <param name="keyValues">主キーのパラメーター。</param>
        /// <returns><typeparamref name="T1"/> レコード。</returns>
        /// <exception cref="ArgumentException">主キーの数とパラメーターの数が一致しません。</exception>
        public T1 GetByPrimaryKey(bool ifThrowNoDataException, params object[] keyValues)
        {
            var query = InstructNoTracking(CreatePrimaryKeyQuery(keyValues));

            if (ifThrowNoDataException)
            {
                return SingleCore(query);
            }

            return SingleOrDefaultCore(query);
        }

        /// <summary>
        /// 主キーを条件としたレコードを取得します。
        /// </summary>
        /// <param name="ifThrowNoDataException">レコードが存在しなかったときにスローするかどうか。</param>
        /// <param name="cancellationToken"> キャンセルトークン。</param>
        /// <param name="keyValues">主キーのパラメーター。</param>
        /// <returns><typeparamref name="T1"/> レコード。</returns>
        /// <exception cref="ArgumentException">主キーの数とパラメーターの数が一致しません。</exception>
        public async Task<T1> GetByPrimaryKeyAsync(bool ifThrowNoDataException,
            CancellationToken cancellationToken = default, params object[] keyValues)
        {
            var query = InstructNoTracking(CreatePrimaryKeyQuery(keyValues));

            if (ifThrowNoDataException)
            {
                return await SingleCoreAsync(cancellationToken, query);
            }

            return await SingleOrDefaultCoreAsync(cancellationToken, query);
        }

        /// <summary>
        /// <see cref="AllwaysNoTracking"/> に応じて <see cref="EntityFrameworkQueryableExtensions.AsNoTracking{T1}(IQueryable{T1})"/> の指示を行います。
        /// </summary>
        protected IQueryable<T1> InstructNoTracking(IQueryable<T1> query)
        {
            if (AllwaysNoTracking)
            {
                return query.AsNoTracking();
            }

            return query;
        }

        /// <summary>
        /// 1件のレコードを取得するための基本命令を呼び出します。<br/>
        /// 既定では <see cref="Queryable.Single{TSource}(IQueryable{TSource})"/> を実施します。<br/>
        /// データベースエンジンに依存して動作を意図的に変更したいときにオーバーライドしてください。
        /// </summary>
        /// <param name="query">実行クエリ。</param>
        /// <returns>レコード。</returns>
        protected virtual T1 SingleCore(IQueryable<T1> query)
        {
            return query.Single();
        }

        /// <summary>
        /// 1件のレコードを取得するための基本命令を呼び出します。<br/>
        /// 既定では <see cref="Queryable.SingleOrDefault{TSource}(IQueryable{TSource})"/> を実施します。<br/>
        /// データベースエンジンに依存して動作を意図的に変更したいときにオーバーライドしてください。
        /// </summary>
        /// <param name="query">実行クエリ。</param>
        /// <returns>レコード。</returns>
        protected virtual T1 SingleOrDefaultCore(IQueryable<T1> query)
        {
            return query.SingleOrDefault();
        }

        /// <summary>
        /// 1件のレコードを取得するための基本命令を呼び出します。<br/>
        /// 基底では <see cref="EntityFrameworkQueryableExtensions.SingleAsync{TSource}(IQueryable{TSource}, CancellationToken)"/> を実施します<br/>
        /// データベースエンジンに依存して動作を意図的に変更したいときにオーバーライドしてください。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン。</param>
        /// <param name="query">実行クエリ。</param>
        /// <returns>レコード。</returns>
        protected virtual async Task<T1> SingleCoreAsync(CancellationToken cancellationToken, IQueryable<T1> query)
        {
            return await query.SingleAsync(cancellationToken);
        }

        /// <summary>
        /// 1件のレコードを取得するための基本命令を呼び出します。<br/>
        /// 基底では <see cref="EntityFrameworkQueryableExtensions.SingleOrDefaultAsync{TSource}(IQueryable{TSource}, CancellationToken)"/> を実施します<br/>
        /// データベースエンジンに依存して動作を意図的に変更したいときにオーバーライドしてください。
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン。</param>
        /// <param name="query">実行クエリ。</param>
        /// <returns>レコード。</returns>
        protected virtual async Task<T1> SingleOrDefaultCoreAsync(CancellationToken cancellationToken, IQueryable<T1> query)
        {
            return await query.SingleOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// 指定された主キーを条件とするクエリを生成します。
        /// </summary>
        /// <param name="keyValues">主キーのパラメーター。</param>
        /// <returns>主キーを条件としたクエリ。</returns>
        /// <exception cref="ArgumentException">主キーの数とパラメーターの数が一致しません。</exception>
        protected IQueryable<T1> CreatePrimaryKeyQuery(params object[] keyValues)
        {
            var entityType = DbContext.Model.FindEntityType(typeof(T1));
            var primaryKey = entityType.FindPrimaryKey();
            var keyProperties = primaryKey.Properties.ToList();

            if (keyProperties.Count != keyValues.Length)
            {
                throw new ArgumentException("The number of primary key values ​​does not match.");
            }

            var query = DbContext.Set<T1>().AsQueryable();

            // 主キーをすべて条件に割り当てる
            for (int i = 0; i < keyProperties.Count; i++)
            {
                var property = keyProperties[i];
                var value = keyValues[i];

                query = query.Where(e => EF.Property<object>(e, property.Name).Equals(value));
            }

            return query;
        }

        /// <summary>
        /// すべてのレコードを取得します。
        /// </summary>
        /// <returns><typeparamref name="T1"/> レコードコレクション。</returns>
        public List<T1> GetAll()
        {
            if (AllwaysNoTracking)
            {
                return DbContext.Set<T1>()
                    .AsNoTracking()
                    .ToList();
            }
            else
            {
                return DbContext.Set<T1>()
                    .ToList();
            }
        }

        /// <summary>
        /// すべてのレコードを取得します。
        /// </summary>
        /// <param name="cancellationToken"> キャンセルトークン。</param>
        /// <returns><typeparamref name="T1"/> レコードコレクション。</returns>
        public async Task<List<T1>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if (AllwaysNoTracking)
            {
                return await DbContext.Set<T1>()
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
            }
            else
            {
                return await DbContext.Set<T1>()
                    .ToListAsync(cancellationToken);
            }
        }

        /// <summary>
        /// エンティティを追加します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public int Add(T1 entity)
        {
            ExecutingAdd(entity);
            DbContext.Set<T1>().Add(entity);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティを追加します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <param name="cancellationToken"> キャンセルトークン。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> AddAsync(T1 entity, CancellationToken cancellationToken = default)
        {
            ExecutingAdd(entity);
            await DbContext.Set<T1>().AddAsync(entity, cancellationToken);
            var result = await DbContext.SaveChangesAsync(cancellationToken);
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを追加します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int AddRange(IEnumerable<T1> entities)
        {
            foreach (var entity in entities)
            {
                ExecutingAdd(entity);
            }
            DbContext.Set<T1>().AddRange(entities);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを追加します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <param name="cancellationToken"> キャンセルトークン。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> AddRangeAsync(IEnumerable<T1> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                ExecutingAdd(entity);
            }
            await DbContext.Set<T1>().AddRangeAsync(entities, cancellationToken);
            var result = await DbContext.SaveChangesAsync(cancellationToken);
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティを更新します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public int Update(T1 entity)
        {
            ExecutingUpdate(entity);
            DbContext.Set<T1>().Update(entity);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティを更新します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <param name="cancellationToken"> キャンセルトークン。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> UpdateAsync(T1 entity, CancellationToken cancellationToken = default)
        {
            ExecutingUpdate(entity);
            DbContext.Set<T1>().Update(entity);
            var result = await DbContext.SaveChangesAsync(cancellationToken);
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを更新します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int UpdateRange(IEnumerable<T1> entities)
        {
            foreach (var entity in entities)
            {
                ExecutingUpdate(entity);
            }
            DbContext.Set<T1>().UpdateRange(entities);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを更新します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <param name="cancellationToken"> キャンセルトークン。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> UpdateRangeAsync(IEnumerable<T1> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                ExecutingUpdate(entity);
            }
            DbContext.Set<T1>().UpdateRange(entities);
            var result = await DbContext.SaveChangesAsync(cancellationToken);
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティを削除します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <returns>影響レコード件数。</returns>
        public int Remove(T1 entity)
        {
            DbContext.Set<T1>().Remove(entity);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティを削除します。
        /// </summary>
        /// <param name="entity">エンティティ。</param>
        /// <param name="cancellationToken"> キャンセルトークン。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> RemoveAsync(T1 entity, CancellationToken cancellationToken = default)
        {
            DbContext.Set<T1>().Remove(entity);
            var result = await DbContext.SaveChangesAsync(cancellationToken);
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを削除します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <returns>影響レコード件数。</returns>
        public int RemoveRange(IEnumerable<T1> entities)
        {
            DbContext.Set<T1>().RemoveRange(entities);
            var result = DbContext.SaveChanges();
            InstructClearChangeTracker();
            return result;
        }

        /// <summary>
        /// エンティティのコレクションを削除します。
        /// </summary>
        /// <param name="entities">エンティティのコレクション。</param>
        /// <param name="cancellationToken"> キャンセルトークン。</param>
        /// <returns>影響レコード件数。</returns>
        public async Task<int> RemoveRangeAsync(IEnumerable<T1> entities, CancellationToken cancellationToken = default)
        {
            DbContext.Set<T1>().RemoveRange(entities);
            var result = await DbContext.SaveChangesAsync(cancellationToken);
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
        /// <see cref="AllwaysNoTracking"/> に応じて変更追跡のクリアを行います。
        /// </summary>
        protected void InstructClearChangeTracker()
        {
            if (AllwaysNoTracking)
            {
                ClearChangeTracker();
            }
        }

        /// <summary>
        /// 追加を行うときに呼び出されます。
        /// </summary>
        /// <param name="entity">追加を行うエンティティ。</param>
        protected virtual void ExecutingAdd(T1 entity) { }

        /// <summary>
        /// 更新を行うときに呼び出されます。
        /// </summary>
        /// <param name="entity">更新を行うエンティティ。</param>
        protected virtual void ExecutingUpdate(T1 entity) { }
    }
}
