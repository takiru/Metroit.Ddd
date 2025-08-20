using System.Collections.Generic;

namespace Metroit.Ddd.ContentRoot
{
    /// <summary>
    /// JSONからDIを読み込むためのルート設定クラスです。
    /// </summary>
    public class DiRootConfig
    {
        /// <summary>
        /// アプリケーション全体のDIコンテナに登録するロガーの名前のリストを取得または設定します。
        /// </summary>
        public List<string> Loggers { get; set; }

        /// <summary>
        /// アプリケーション全体のDIコンテナに登録するサービスのリストを取得または設定します。
        /// </summary>
        public List<DiServiceConfig> Services { get; set; }

        /// <summary>
        /// アプリケーション全体のDIコンテナに登録するDbContextのリストを取得または設定します。
        /// </summary>
        public List<DiDbContextConfig> DbContexts { get; set; }
    }
}
