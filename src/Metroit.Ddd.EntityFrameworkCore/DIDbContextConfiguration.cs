using Metroit.Ddd.CompositionRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Metroit.Ddd.EntityFrameworkCore
{
    /// <summary>
    /// JSONから読み込まれたDI情報から、データベースコンテキストの構成を提供するための抽象クラスです。
    /// </summary>
    public abstract class DIDbContextConfiguration : IDIDbContextConfiguration
    {
        /// <summary>
        /// データベースコンテキストの構成を行います。
        /// </summary>
        /// <param name="services">サービス構成ビルダー。</param>
        /// <param name="dbContextConfig">JSONのDbContext情報。</param>
        /// <param name="dbContextType">DbContextタイプ。</param>
        /// <param name="connectionString">接続文字列。</param>
        /// <param name="lifetime">サービスライフタイム。</param>
        public void Configure(IServiceCollection services, DiDbContextConfig dbContextConfig,
            Type dbContextType, string connectionString, ServiceLifetime lifetime)
        {
            // AddDbContext<TContext>(IServiceCollection, Action<IServiceProvider,DbContextOptionsBuilder>, ServiceLifetime, ServiceLifetime) メソッドを求める
            var method = typeof(EntityFrameworkServiceCollectionExtensions)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(
                    m => m.Name == "AddDbContext" &&
                    m.IsGenericMethod &&
                    m.GetGenericMethodDefinition().GetGenericArguments().Count() == 1 &&
                    m.GetParameters().Length == 4 &&
                    m.GetParameters()[1].ParameterType.GenericTypeArguments.Length == 2
                    )
                .FirstOrDefault();

            var genericMethod = method?.MakeGenericMethod(dbContextType);
            if (genericMethod == null)
            {
                return;
            }

            genericMethod.Invoke(null, new object[]
            {
                services,
                (Action<IServiceProvider, DbContextOptionsBuilder>)((serviceProvider, options) =>
                {
                    AddDbContext(dbContextConfig, connectionString, serviceProvider, options);

                    if (dbContextConfig.Options?.TryGetValue("EnableSensitiveDataLogging", out var logging) == true &&
                        Convert.ToBoolean(logging))
                    {
                        options.EnableSensitiveDataLogging();
                    }
                }),
                lifetime,
                lifetime
            });
        }

        /// <summary>
        /// 具体的なデータベースを用いてデータベースコンテキストを追加します。
        /// </summary>
        /// <param name="dbContextConfig">JSONのDbContext情報。</param>
        /// <param name="connectionString">接続文字列。</param>
        /// <param name="serviceProvider">サービスプロバイダー。</param>
        /// <param name="options">接続オプション。</param>
        protected abstract void AddDbContext(DiDbContextConfig dbContextConfig, string connectionString,
            IServiceProvider serviceProvider, DbContextOptionsBuilder options);
    }
}
