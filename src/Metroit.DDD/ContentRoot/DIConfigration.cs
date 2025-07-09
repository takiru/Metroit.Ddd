using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Metroit.DDD.ContentRoot
{
    /// <summary>
    /// アプリケーション全体のDI情報を提供します。
    /// </summary>
    public class DIConfigration
    {
        /// <summary>
        /// アプリケーション全体のDIコンテナを取得します。
        /// </summary>
        public static IHost Host { get; private set; }

        public DIConfigration()
        {
            
        }

        public DIConfigration(string appsettingsJsonFile = default)
        {
            
        }

        /// <summary>W
        /// アプリケーション全体のDI登録を行います。
        /// </summary>
        public static void Configure()
        {
            //Configuration = GetConfiguration();

            Host = Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder(Environment.GetCommandLineArgs())
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    //// DIコンテナの登録を行う。
                    //ApplicationBaseDIConfiguration.Configure(services, Configuration);
                    //RepositoryDIConfiguration.Configure(services, Configuration);
                    //ServiceDIConfiguration.Configure(services, Configuration);
                    //ViewDIConfiguration.Configure(services, Configuration);
                })
                .Build();
            //Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            //    .ConfigureServices((context, services) =>
            //    {
            //        ApplicationBaseDIConfiguration.Configure(services, Configuration);
            //        RepositoryDIConfiguration.Configure(services, Configuration);
            //        ServiceDIConfiguration.Configure(services, Configuration);
            //        ViewDIConfiguration.Configure(services, Configuration);
            //    })
            //.Build();
        }
    }
}
