using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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


        /// <summary>W
        /// アプリケーション全体のDI登録を行います。
        /// </summary>
        public static void Configure()
        {
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
                    PrepareFirstServiceConfiguration?.Invoke(context, services);
                    DomainServiceConfiguration?.Invoke(context, services);
                    InfrastructureServiceConfiguration?.Invoke(context, services);
                    ApplicationServiceConfiguration?.Invoke(context, services);
                    PresentationServiceConfiguration?.Invoke(context, services);
                    PrepareLastServiceConfiguration?.Invoke(context, services);
                })
                .Build();
        }

        /// <summary>
        /// 最初に準備が必要なサービス構成を行います。
        /// </summary>
        public static Action<HostBuilderContext, IServiceCollection> PrepareFirstServiceConfiguration = null;

        /// <summary>
        /// ドメイン層で必要なサービス構成を行います。
        /// </summary>
        public static Action<HostBuilderContext, IServiceCollection> DomainServiceConfiguration = null;

        /// <summary>
        /// インフラ層で必要なサービス構成を行います。
        /// </summary>
        public static Action<HostBuilderContext, IServiceCollection> InfrastructureServiceConfiguration = null;

        /// <summary>
        /// アプリケーション層で必要なサービス構成を行います。
        /// </summary>
        public static Action<HostBuilderContext, IServiceCollection> ApplicationServiceConfiguration = null;

        /// <summary>
        /// プレゼンテーション層で必要なサービス構成を行います。
        /// </summary>
        public static Action<HostBuilderContext, IServiceCollection> PresentationServiceConfiguration = null;

        /// <summary>
        /// 最後に準備が必要なサービス構成を行います。
        /// </summary>
        public static Action<HostBuilderContext, IServiceCollection> PrepareLastServiceConfiguration = null;
    }
}
