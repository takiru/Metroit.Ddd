using System.Windows.Forms;

namespace Metroit.Mvvm.WinForms.ViewModels
{
    /// <summary>
    /// メッセージを提供します。
    /// </summary>
    public class MessageProvider
    {
        /// <summary>
        /// 情報メッセージ を出力するためのデリゲートを取得します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public delegate void InformationMessage(string message);

        /// <summary>
        /// 情報メッセージ を出力します。
        /// </summary>
        public InformationMessage ExecuteInformationMessage = null;

        /// <summary>
        /// 確認メッセージ を出力するためのデリゲートを取得します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="buttons">ボタンの種類。</param>
        public delegate DialogResult ConfirmMessage(string message, MessageBoxButtons buttons);

        /// <summary>
        /// 確認メッセージ を出力します。
        /// </summary>
        public ConfirmMessage ExecuteConfirmMessage = null;

        /// <summary>
        /// 警告メッセージ を出力するためのデリゲートを取得します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public delegate void WarningMessage(string message);

        /// <summary>
        /// 警告メッセージ を出力します。
        /// </summary>
        public WarningMessage ExecuteWarningMessage = null;

        /// <summary>
        /// エラーメッセージ を出力するためのデリゲートを取得します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public delegate void ErrorMessage(string message);

        /// <summary>
        /// エラーメッセージ を出力します。
        /// </summary>
        public ErrorMessage ExecuteErrorMessage = null;
    }
}
