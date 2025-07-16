using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Metroit.Mvvm.Extensions
{
    /// <summary>
    /// ReactiveProperty による値、状態のバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class LabelExtensions
    {
        /// <summary>
        /// ラベルの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label">ラベルオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this Label label, Expression<Func<T>> expression)
        {
            PropertyBindExtensions.Bind(() => label.Text, expression);
        }
    }
}
