using Microsoft.EntityFrameworkCore;

namespace Metroit.DDD.Infrastructure.EF
{
    /// <summary>
    /// Entity Framework Coreのリポジトリの基本操作を提供します。
    /// </summary>
    /// <typeparam name="T">エンティティクラス。</typeparam>
    public abstract class EFServiceBase<T> where T : class
    {
        protected readonly DbContext DbContext;

        public EFServiceBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
