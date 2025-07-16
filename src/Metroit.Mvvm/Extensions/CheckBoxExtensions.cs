using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Metroit.Mvvm.Extensions
{
    /// <summary>
    /// ReactiveProperty による値、状態のバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class CheckBoxExtensions
    {
        /// <summary>
        /// チェックボックスのチェック状態をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="checkBox">チェックボックスオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindChecked<T>(this CheckBox checkBox, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => checkBox.Checked, expression);
        }

        /// <summary>
        /// チェックボックスの状態をバインドします。
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
