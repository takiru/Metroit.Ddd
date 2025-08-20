using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Metroit.Ddd.ContentRoot
{
    /// <summary>
    /// JSON構成からDIサービスを登録する拡張メソッドを提供します。
    /// </summary>
    public static class ServiceCollectionJsonExtensions
    {
        /// <summary>
        /// JSON構成からDIサービスを登録します。
        /// </summary>
        /// <param name="services">サービス構成ビルダー。</param>
        /// <param name="context">コンテキスト。</param>
        /// <param name="loggerConfigurations">ロギングの構成を提供するためのインターフェース。</param>
        /// <param name="dbContextConfigurations">データベースコンテキストの構成を提供するためのインターフェイス。</param>
        public static void RegisterFromJsonConfig(this IServiceCollection services, HostBuilderContext context,
            Dictionary<string, IDILoggerConfiguration> loggerConfigurations,
            Dictionary<string, IDIDbContextConfiguration> dbContextConfigurations)
        {
            var diConfig = ReadAllDiConfig(context.Configuration);
            if (diConfig == null)
            {
                return;
            }

            RegisterLoggers(services, loggerConfigurations, diConfig.Loggers);
            RegisterServices(services, diConfig.Services);
            RegisterDbContexts(context, services, dbContextConfigurations, diConfig.DbContexts);
        }

        /// <summary>
        /// JSON構成から全てのDI設定を読み込む。
        /// </summary>
        /// <param name="configuration">構成情報。</param>
        /// <returns>JSON構成からDI設定を読み込んだオブジェクト。JSON構成がなかったときは null を返却する。</returns>
        private static DiRootConfig ReadAllDiConfig(IConfiguration configuration)
        {
            var diConfigs = configuration.AsEnumerable()
                .Where(x => IsValidDISection(x.Key))
                .Select(y => configuration.GetSection(y.Key).Get<DiRootConfig>());
            if (diConfigs.Count() == 0)
            {
                return null;
            }

            var diConfig = new DiRootConfig();
            foreach (var d in diConfigs)
            {
                if (d.Loggers != null)
                {
                    if (diConfig.Loggers == null)
                    {
                        diConfig.Loggers = new List<string>();
                    }
                    diConfig.Loggers.AddRange(d.Loggers);
                }
                if (d.Services != null)
                {
                    if (diConfig.Services == null)
                    {
                        diConfig.Services = new List<DiServiceConfig>();
                    }
                    diConfig.Services.AddRange(d.Services);
                }
                if (d.DbContexts != null)
                {
                    if (diConfig.DbContexts == null)
                    {
                        diConfig.DbContexts = new List<DiDbContextConfig>();
                    }
                    diConfig.DbContexts.AddRange(d.DbContexts);
                }
            }

            return diConfig;
        }

        /// <summary>
        /// 有効なDIセクションであることを取得する。
        /// DIセクションのキーは "DependencyInjection" で始まり、コロンがないこと。
        /// </summary>
        /// <param name="key">キー。</param>
        /// <returns>有効なDIセクションの場合は true, それ以外は false を返却する。</returns>
        private static bool IsValidDISection(string key)
        {
            if (!key.StartsWith("DependencyInjection"))
            {
                return false;
            }

            if (key.Contains(':'))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// JSONのロガー情報からロガーを登録する。
        /// </summary>
        /// <param name="services">サービス構成ビルダー。</param>
        /// <param name="loggerConfigurations">ロギングの構成を提供するためのインターフェース。</param>
        /// <param name="loggers">JSONのロガー情報。</param>
        private static void RegisterLoggers(IServiceCollection services,
            Dictionary<string, IDILoggerConfiguration> loggerConfigurations, List<string> loggers)
        {
            if (loggerConfigurations == null)
            {
                return;
            }

            if (loggers == null || loggers.Count == 0)
            {
                return;
            }

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);

                foreach (var logger in loggers)
                {
                    if (!loggerConfigurations.ContainsKey(logger))
                    {
                        continue;
                    }

                    loggerConfigurations[logger].Configure(loggingBuilder);
                }
            });
        }

        /// <summary>
        /// JSONのサービス情報からサービスを登録する。
        /// </summary>
        /// <param name="services">サービス構成ビルダー。</param>
        /// <param name="serviceConfigs">JSONのサービス情報。</param>
        private static void RegisterServices(IServiceCollection services, List<DiServiceConfig> serviceConfigs)
        {
            if (serviceConfigs == null)
            {
                return;
            }

            foreach (var config in serviceConfigs)
            {
                var implType = Type.GetType(config.Implementation);
                if (implType == null)
                {
                    continue;
                }

                Type interfaceType = string.IsNullOrWhiteSpace(config.Interface)
                    ? null
                    : Type.GetType(config.Interface);

                var lifetime = GetServiceLifetime(config.Lifetime);

                if (config.UseInstance)
                {
                    // インスタンスをサービス登録する場合
                    object instance = null;
                    if (config.Args == null || config.Args.Length == 0)
                    {
                        instance = Activator.CreateInstance(implType);
                    }
                    else
                    {
                        instance = Activator.CreateInstance(implType, config.Args);
                    }

                    if (config.Settings != null)
                    {
                        foreach (var kvp in config.Settings)
                        {
                            var prop = implType.GetProperty(kvp.Key);
                            if (prop != null)
                            {
                                var value = Convert.ChangeType(kvp.Value, prop.PropertyType);
                                prop.SetValue(instance, value);
                            }
                        }
                    }

                    if (interfaceType != null)
                    {
                        services.Add(new ServiceDescriptor(interfaceType, instance));
                    }
                    else
                    {
                        services.Add(new ServiceDescriptor(implType, instance));
                    }
                }
                else
                {
                    // クラスをサービス登録する場合
                    if (interfaceType != null)
                    {
                        services.Add(new ServiceDescriptor(interfaceType, implType, lifetime));
                    }
                    else
                    {
                        services.Add(new ServiceDescriptor(implType, implType, lifetime));
                    }
                }
            }
        }

        /// <summary>
        /// JSONのDbContext情報からDbContextを登録する。
        /// </summary>
        /// <param name="context">コンテキスト。</param>
        /// <param name="services">サービス構成ビルダー。</param>
        /// <param name="dbContextConfigurations">データベースコンテキストの構成を提供するためのインターフェイス。</param>
        /// <param name="dbContextConfigs">JSONのDbContext情報。</param>
        private static void RegisterDbContexts(HostBuilderContext context, IServiceCollection services,
            Dictionary<string, IDIDbContextConfiguration> dbContextConfigurations, List<DiDbContextConfig> dbContextConfigs)
        {
            if (dbContextConfigs == null)
            {
                return;
            }

            if (dbContextConfigurations == null || dbContextConfigurations.Count() == 0)
            {
                return;
            }

            foreach (var config in dbContextConfigs)
            {
                if (!dbContextConfigurations.ContainsKey(config.ContextName))
                {
                    continue;
                }

                var configuration = dbContextConfigurations[config.ContextName];

                var contextType = Type.GetType(config.TypeName);
                if (contextType == null)
                {
                    continue;
                }

                var connectionString = context.Configuration.GetConnectionString(config.ConnectionStringName);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    continue;
                }

                var lifetime = GetDbContextServiceLifetime(config.Lifetime);

                configuration.Configure(services, config, contextType, connectionString, lifetime);
            }
        }

        /// <summary>
        /// サービスライフタイムを取得します。
        /// </summary>
        /// <param name="lifetime">ライフタイム名。</param>
        /// <returns>ライフタイム。</returns>
        private static ServiceLifetime GetServiceLifetime(string lifetime)
        {
            switch (lifetime.ToLower())
            {
                case "singleton":
                    return ServiceLifetime.Singleton;

                case "scoped":
                    return ServiceLifetime.Scoped;

                case "transient":
                    return ServiceLifetime.Transient;

                default:
                    return ServiceLifetime.Transient;
            }
        }

        /// <summary>
        /// DbContext向けのサービスライフタイムを取得します。
        /// </summary>
        /// <param name="lifetime">ライフタイム名。</param>
        /// <returns>ライフタイム。</returns>
        private static ServiceLifetime GetDbContextServiceLifetime(string lifetime)
        {
            switch (lifetime.ToLower())
            {
                case "singleton":
                    return ServiceLifetime.Singleton;

                case "scoped":
                    return ServiceLifetime.Scoped;

                case "transient":
                    return ServiceLifetime.Transient;

                default:
                    return ServiceLifetime.Scoped;
            }
        }
    }

}
