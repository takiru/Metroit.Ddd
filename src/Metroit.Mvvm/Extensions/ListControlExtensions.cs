using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Metroit.Mvvm.Extensions
{
    /// <summary>
    /// ReactiveProperty による値、状態のバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class ListControlExtensions
    {
        /// <summary>
        /// リストコントロールのデータソースバインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listControl">リストコントロールオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        /// <param name="valueMember">値のメンバ名。</param>
        /// <param name="displayMenber">表示値のメンバ名。</param>
        public static void BindDataSource<T>(this ListControl listControl, Expression<Func<T>> expression, string valueMember, string displayMenber)
        {
            PropertyBindExtensions.Bind(() => listControl.DataSource, expression);
            listControl.ValueMember = valueMember;
            listControl.DisplayMember = displayMenber;
        }
    }
}
