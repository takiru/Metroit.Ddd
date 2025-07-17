using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Metroit.Ddd.ContentRoot
{
    /// <summary>
    /// アプリケーション全体のDI情報を提供します。
    /// </summary>
    public class DIConfigration
    {
        /// <summary>
        /// アプリケーション全体のDIコンテナを取得します。
        /// </summary>
        public IHost Host { get; private set; }

        /// <summary>
        /// アプリケーション全体のDI登録を行います。
        /// </summary>
        public void Configure()
        {
            Host = Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder(Environment.GetCommandLineArgs())
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;

                    config
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    OnConfiguringServices(new DIConfigurationBuilder(context, services));
                })
                .Build();
        }

        /// <summary>
        /// サービスを構成します。
        /// </summary>
        /// <param name="configurationBuilder">サービスを構成するために使用されるビルダー。</param>
        protected virtual void OnConfiguringServices(DIConfigurationBuilder configurationBuilder) { }
    }
}
