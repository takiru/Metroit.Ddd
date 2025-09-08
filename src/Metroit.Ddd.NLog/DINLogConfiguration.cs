using Metroit.Ddd.CompositionRoot;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Metroit.Ddd.NLog
{
    /// <summary>
    /// NLogを使用したロギングの構成を提供するクラスです。
    /// </summary>
    public class DINLogConfiguration : IDILoggerConfiguration
    {
        /// <summary>
        /// NLogを使用したロギングの構成を行います。
        /// </summary>
        /// <param name="loggingBuilder">ロギングビルダー。</param>
        public void Configure(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddNLog();
        }
    }
}
