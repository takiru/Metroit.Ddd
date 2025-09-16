using Metroit.Ddd.CompositionRoot;
using Microsoft.EntityFrameworkCore;
using System;

namespace Metroit.Ddd.EntityFrameworkCore.Oracle
{
    /// <summary>
    /// Oracleを使用したデータベースコンテキスト構成を提供します。
    /// </summary>
    public class DIOracleConfiguration : DIDbContextConfiguration
    {
        /// <summary>
        /// 具体的なデータベースを用いてデータベースコンテキストを追加します。
        /// </summary>
        /// <param name="dbContextConfig">JSONのDbContext情報。</param>
        /// <param name="connectionString">接続文字列。</param>
        /// <param name="serviceProvider">サービスプロバイダー。</param>
        /// <param name="options">接続オプション。</param>
        protected override void AddDbContext(DiDbContextConfig dbContextConfig, string connectionString,
            IServiceProvider serviceProvider, DbContextOptionsBuilder options)
        {
            options.UseOracle(connectionString, sqlOptions =>
            {
                if (dbContextConfig.Options?.TryGetValue("CommandTimeout", out var timeout) == true)
                {
                    sqlOptions.CommandTimeout(Convert.ToInt32(timeout));
                }
#if NETSTANDARD2_0_OR_GREATER
                if (dbContextConfig.Options?.TryGetValue("UseOracleSQLCompatibility", out var compatibility) == true)
                {
                    sqlOptions.UseOracleSQLCompatibility((string)(compatibility));
                }
#endif
            });
        }
    }
}
