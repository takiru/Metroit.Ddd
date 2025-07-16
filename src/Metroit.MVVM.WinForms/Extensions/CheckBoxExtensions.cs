using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Metroit.Mvvm.WinForms.Extensions
{
    /// <summary>
    /// チェックボックスのバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class CheckBoxExtensions
    {
        /// <summary>
        /// チェック状態をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="checkBox">チェックボックスオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindChecked<T>(this CheckBox checkBox, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => checkBox.Checked, expression);
        }

        /// <summary>
        /// 状態をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="checkBox">チェックボックスオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindCheckState<T>(this CheckBox checkBox, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => checkBox.CheckState, expression);
        }
    }
}
