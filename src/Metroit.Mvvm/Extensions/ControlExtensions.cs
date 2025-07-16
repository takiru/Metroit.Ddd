using System;
using System.Linq.Expressions;
using System.Windows.Forms;
using Metroit.Mvvm.Extensions;

namespace Metroit.Mvvm.Extensions
{
    /// <summary>
    /// ReactiveProperty による値、状態のバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// コントロールのテキストをバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindText<T>(this Control control, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => control.Text, expression);
        }

        /// <summary>
        /// コントロールの活性バインドを行います。
        /// </summary>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindEnabled<T>(this Control control, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => control.Enabled, expression);
        }

        /// <summary>
        /// コントロールの表示バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindVisible<T>(this Control control, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => control.Visible, expression);
        }
    }
}
