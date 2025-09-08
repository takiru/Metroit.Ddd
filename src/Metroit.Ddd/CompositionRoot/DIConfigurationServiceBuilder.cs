using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Metroit.Ddd.CompositionRoot
{
    /// <summary>
    /// DI構成を行うための情報を提供します。
    /// </summary>
    public class DIConfigurationServiceBuilder
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
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="context"><see cref="IHost"/> 上の共通サービスを含むコンテキスト。</param>
        /// <param name="services">サービス記述子のコレクション。</param>
        internal DIConfigurationServiceBuilder(HostBuilderContext context, IServiceCollection services)
        {
            Context = context;
            Services = services;
        }
    }
}
