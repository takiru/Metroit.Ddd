using Metroit.Ddd.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Metroit.Ddd.EntityFrameworkCore
{
    /// <summary>
    /// Entity Framework LINQ 関連の拡張メソッドを提供します。
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// <see cref="EntityFrameworkQueryableExtensions.AsNoTracking{TEntity}(IQueryable{TEntity})"/> を条件付きで実行します。
        /// </summary>
        /// <typeparam name="T">クエリ対象のエンティティの種類。</typeparam>
        /// <param name="source">ソースクエリ。</param>
        /// <param name="noTracking"></param>
        /// <returns>
        /// <paramref name="noTracking"/> が <see langword="true"/> のとき、NoTracking が適用された新しいクエリ、または NoTracking がサポートされていない場合はソースクエリを返却します。<br/>
        /// <see langword="false"/> のときはソースクエリを返却します。
        /// </returns>
        public static IQueryable<T> AsNoTracking<T>(this IQueryable<T> source, bool noTracking) where T : class
        {
            if (noTracking)
            {
                return source.AsNoTracking();
            }

            return source;
        }
    }
}
