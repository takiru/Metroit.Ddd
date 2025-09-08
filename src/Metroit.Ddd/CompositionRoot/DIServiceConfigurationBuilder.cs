using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Metroit.Ddd.CompositionRoot
{
    /// <summary>
    /// DI構成の構築を提供します。
    /// </summary>
    public class DIServiceConfigurationBuilder
    {
        /// <summary>
        /// <see cref="IHost"/> 上の共通サービスを含むコンテキストを取得します。
        /// </summary>
        public HostBuilderContext Context { get; }

        /// <summary>
        /// サービス記述子のコレクションを取得します。
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 構成の適用に利用するサービスビルダー。
        /// </summary>
        private readonly DIConfigurationServiceBuilder Builder;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="context">コンテキスト。</param>
        /// <param name="services">サービス構成ビルダー。</param>
        public DIServiceConfigurationBuilder(HostBuilderContext context, IServiceCollection services)
        {
            Context = context;
            Services = services;
            Builder = new DIConfigurationServiceBuilder(Context, services);
        }

        /// <summary>
        /// 構成の適用を行います。
        /// </summary>
        /// <param name="configuration">サービス構成。</param>
        /// <returns>DI構成。</returns>
        public virtual DIServiceConfigurationBuilder ApplyConfiguration(IDIServiceConfiguration configuration)
        {
            configuration.Configure(Builder);
            return this;
        }
    }
}
