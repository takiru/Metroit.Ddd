using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Metroit.Ddd.ContentRoot
{
    /// <summary>
    /// DI構成の構築を提供します。
    /// </summary>
    public class DIConfigurationBuilder
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
        private DIConfigurationServiceBuilder _builder;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="context"><see cref="IHost"/> 上の共通サービスを含むコンテキスト。</param>
        /// <param name="services">サービス記述子のコレクション。</param>
        public DIConfigurationBuilder(HostBuilderContext context, IServiceCollection services)
        {
            Context = context;
            Services = services;
            _builder = new DIConfigurationServiceBuilder(Context, services);
        }

        /// <summary>
        /// 構成の適用を行います。
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public virtual DIConfigurationBuilder ApplyConfiguration(IDITypeConfiguration configuration)
        {
            configuration.Configure(_builder);
            return this;
        }
    }
}
