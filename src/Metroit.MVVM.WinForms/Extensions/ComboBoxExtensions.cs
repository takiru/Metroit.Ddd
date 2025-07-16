using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Metroit.Mvvm.WinForms.Extensions
{
    /// <summary>
    /// コンボボックスのバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class ComboBoxExtensions
    {
        /// <summary>
        /// 選択値をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox">コンボボックスオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindSelectedItem<T>(this ComboBox comboBox, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => comboBox.SelectedItem, expression);
        }
    }
}
