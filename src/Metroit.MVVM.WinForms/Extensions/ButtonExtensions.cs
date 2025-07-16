using System.Windows.Forms;
using System.Windows.Input;

namespace Metroit.Mvvm.WinForms.Extensions
{
    /// <summary>
    /// ボタンのバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class ButtonExtensions
    {
        /// <summary>
        /// クリックをバインドします。
        /// </summary>
        /// <param name="button">ボタンオブジェクト。</param>
        /// <param name="command">コマンド。</param>
        public static void BindClick(this Button button, ICommand command)
        {
#if NET7_0_OR_GREATER
            button.Command = command;
#else
            ControlExtensions.BindClick(button, command);
#endif
        }
    }
}
