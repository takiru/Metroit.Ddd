using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Metroit.Ddd.CompositionRoot
{
    /// <summary>
    /// アプリケーション全体のDI情報を提供します。
    /// </summary>
    public class DIConfigration
    {
        /// <summary>
        /// アプリケーション構成で読み込むJSONストリームのコレクションを取得または設定します。
        /// </summary>
        public IEnumerable<Stream> JsonStreams { get; set; } = null;

        /// <summary>
        /// アプリケーション構成で読み込むJSONファイルのコレクションを取得または設定します。
        /// </summary>
        public IEnumerable<string> JsonFiles { get; set; } = null;

        /// <summary>
        /// データベースコンテキストの構成を提供するためのインターフェイスを取得または設定します。
        /// </summary>
        public Dictionary<string, IDIDbContextConfiguration> DbContextConfigurations { get; set; } = null;

        /// <summary>
        /// ロギングの構成を提供するためのインターフェイスを取得または設定します。
        /// </summary>
        public Dictionary<string, IDILoggerConfiguration> LoggerConfigurations { get; set; } = null;

        /// <summary>
        /// アプリケーション全体のDIコンテナを取得します。
        /// </summary>
        public IHost Host { get; private set; }

        /// <summary>
        /// アプリケーション全体のDI登録を行います。
        /// </summary>
        public void Configure()
        {
            var hostBuilder = Microsoft.Extensions.Hosting.Host.
                CreateDefaultBuilder(Environment.GetCommandLineArgs());

            Host = hostBuilder
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;

                    config
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    if (JsonFiles != null)
                    {
                        foreach (var jsonFile in JsonFiles)
                        {
                            config.AddJsonFile(jsonFile, optional: true, reloadOnChange: true);
                        }
                    }
                    if (JsonStreams != null)
                    {
                        foreach (var jsonStream in JsonStreams)
                        {
                            config.AddJsonStream(jsonStream);
                        }
                    }
                    ConfigureAppConfiguration(context, config);
                })
                .ConfigureLogging((context, logging) =>
                {
                    ConfigureLogging(context, logging);
                })
                .ConfigureServices((context, services) =>
                {
                    services.RegisterFromJsonConfig(context, LoggerConfigurations, DbContextConfigurations);
                    ConfigureServices(context, services);
                    OnServiceConfiguring(new DIServiceConfigurationBuilder(context, services));
                })
                .Build();
        }

        /// <summary>
        /// アプリケーションの構成を設定します。
        /// </summary>
        /// <param name="context">コンテキスト。</param>
        /// <param name="configurationBuilder">アプリケーション構成ビルダー。</param>
        protected virtual void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder configurationBuilder) { }

        /// <summary>
        /// アプリケーションのロギングを設定します。
        /// </summary>
        /// <param name="context">コンテキスト。</param>
        /// <param name="loggingBuilder">ロギング構成ビルダー。</param>
        protected virtual void ConfigureLogging(HostBuilderContext context, ILoggingBuilder loggingBuilder) { }

        /// <summary>
        /// サービスを構成します。
        /// </summary>
        /// <param name="context">コンテキスト。</param>
        /// <param name="services">サービス構成ビルダー。</param>
        protected virtual void ConfigureServices(HostBuilderContext context, IServiceCollection services) { }

        /// <summary>
        /// サービスを構成します。
        /// </summary>
        /// <param name="diServiceConfigurationBuilder">サービス構成ビルダー。</param>
        protected virtual void OnServiceConfiguring(DIServiceConfigurationBuilder diServiceConfigurationBuilder) { }
    }
}
