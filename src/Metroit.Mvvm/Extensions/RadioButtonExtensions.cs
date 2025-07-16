using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Metroit.Mvvm.Extensions
{
    /// <summary>
    /// ReactiveProperty による値、状態のバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class RadioButtonExtensions
    {
        /// <summary>
        /// ラジオボタンのチェック状態をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="radioButton">ラジオボタンオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindChecked<T>(this RadioButton radioButton, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => radioButton.Checked, expression);
        }
    }
}
