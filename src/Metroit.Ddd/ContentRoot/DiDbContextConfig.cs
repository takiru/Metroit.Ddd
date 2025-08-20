using System.Collections.Generic;

namespace Metroit.Ddd.ContentRoot
{
    /// <summary>
    /// /// JSONからDIを読み込むためのデータベースコンテキストの構成を表します。
    /// </summary>
    public class DiDbContextConfig
    {
        /// <summary>
        /// データベースコンテキストの名前を取得または設定します。
        /// </summary>
        public string ContextName { get; set; }

        /// <summary>
        /// データベースコンテキストで利用するタイプ名を取得または設定します。
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// データベース接続文字列の名前を取得または設定します。
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// サービスのライフタイムを取得または設定します。
        /// </summary>
        public string Lifetime { get; set; }

        /// <summary>
        /// データベースコンテキストのオプションを取得または設定します。
        /// </summary>
        public Dictionary<string, object> Options { get; set; }
    }
}
