using System;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Windows.Input;

namespace Metroit.Mvvm.WinForms.Extensions
{
    /// <summary>
    /// コントロールのバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// テキストをバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindText<T>(this Control control, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => control.Text, expression);
        }

        /// <summary>
        /// 活性状態をバインドします。
        /// </summary>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindEnabled<T>(this Control control, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => control.Enabled, expression);
        }

        /// <summary>
        /// 表示状態をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindVisible<T>(this Control control, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => control.Visible, expression);
        }

        /// <summary>
        /// クリックをバインドします。
        /// </summary>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="command">コマンド。</param>
        public static void BindClick(this Control control, ICommand command)
        {
            command.CanExecuteChanged += (sender, args) => control.Enabled = command.CanExecute(null);
            control.Click += (sender, args) => command.Execute(null);

            // 初期状態を決定
            control.Enabled = command.CanExecute(null);
        }
    }
}
