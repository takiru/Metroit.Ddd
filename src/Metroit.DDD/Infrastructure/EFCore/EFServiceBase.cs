using Microsoft.EntityFrameworkCore;

namespace Metroit.DDD.Infrastructure.EFCore
{
    /// <summary>
    /// Entity Framework Coreのサービスの基本操作を提供します。
    /// </summary>
    /// <typeparam name="T">エンティティクラス。</typeparam>
    public abstract class EFServiceBase<T> where T : class
    {
        /// <summary>
        /// 現在利用しているコンテキストを取得します。
        /// </summary>
        protected DbContext DbContext { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="dbContext"><see cref="DbContext"/> オブジェクト。</param>
        public EFServiceBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
