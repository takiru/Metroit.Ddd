using Microsoft.Extensions.DependencyInjection;
using System;

namespace Metroit.Ddd.ContentRoot
{
    /// <summary>
    /// JSONから読み込まれたDI情報から、データベースコンテキストの構成を提供するためのインターフェイスです。
    /// </summary>
    public interface IDIDbContextConfiguration
    {
        /// <summary>
        /// データベースコンテキストの構成を行います。
        /// </summary>
        /// <param name="services">サービス構成ビルダー。</param>
        /// <param name="dbContextConfig">JSONのDbContext情報。</param>
        /// <param name="dbContextType">DbContextタイプ。</param>
        /// <param name="connectionString">接続文字列。</param>
        /// <param name="lifetime">サービスライフタイム。</param>
        void Configure(IServiceCollection services, DiDbContextConfig dbContextConfig, Type dbContextType,
            string connectionString, ServiceLifetime lifetime);
    }
}
