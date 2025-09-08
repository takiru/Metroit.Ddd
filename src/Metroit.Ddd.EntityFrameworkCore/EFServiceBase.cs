using Microsoft.EntityFrameworkCore;

namespace Metroit.Ddd.EntityFrameworkCore
{
    /// <summary>
    /// Entity Framework Coreのサービスの基本操作を提供します。
    /// </summary>
    /// <typeparam name="T">利用する <see cref="DbContext"/> クラス。</typeparam>
    public abstract class EFServiceBase<T> where T : DbContext
    {
        /// <summary>
        /// 現在利用しているコンテキストを取得します。
        /// </summary>
        protected T DbContext { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="dbContext"><see cref="DbContext"/> オブジェクト。</param>
        public EFServiceBase(T dbContext)
        {
            DbContext = dbContext;
        }
    }
}
