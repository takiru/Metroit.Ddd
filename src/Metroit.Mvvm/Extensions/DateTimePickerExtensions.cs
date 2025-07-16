using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Metroit.Mvvm.Extensions
{
    /// <summary>
    /// ReactiveProperty による値、状態のバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class DateTimePickerExtensions
    {
        /// <summary>
        /// 日付ピッカーの値をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dateTimePicker">日付ピッカーオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindValue<T>(this DateTimePicker dateTimePicker, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => dateTimePicker.Value, expression);
        }
    }
}
