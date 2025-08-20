using System.Collections.Generic;

namespace Metroit.Ddd.ContentRoot
{
    /// <summary>
    /// JSONからDIを読み込むためのDIサービスの構成を表します。
    /// </summary>
    public class DiServiceConfig
    {
        /// <summary>
        /// サービスで利用するインターフェースのタイプ名を取得または設定します。
        /// </summary>
        public string Interface { get; set; }

        /// <summary>
        /// サービスで利用する実装のタイプ名を取得または設定します。
        /// </summary>
        public string Implementation { get; set; }

        /// <summary>
        /// サービスのライフタイムを取得または設定します。
        /// </summary>
        public string Lifetime { get; set; }

        /// <summary>
        /// サービスのインスタンスを使用するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool UseInstance { get; set; }

        /// <summary>
        /// インスタンスのコンストラクタ引数を取得または設定します。
        /// </summary>
        public object[] Args { get; set; }

        /// <summary>
        /// インスタンスのプロパティを取得または設定します。
        /// </summary>
        public Dictionary<string, object> Settings { get; set; }
    }
}
