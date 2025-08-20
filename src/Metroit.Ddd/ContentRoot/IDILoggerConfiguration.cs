using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Metroit.Ddd.ContentRoot
{
    /// <summary>
    /// JSONから読み込まれたDI情報から、ロギングの構成を提供するためのインターフェイスです。
    /// </summary>
    public interface IDILoggerConfiguration
    {
        /// <summary>
        /// ロギングの構成を行います。
        /// </summary>
        /// <param name="loggingBuilder">ロギング構成ビルダー。</param>
        void Configure(ILoggingBuilder loggingBuilder);
    }
}
