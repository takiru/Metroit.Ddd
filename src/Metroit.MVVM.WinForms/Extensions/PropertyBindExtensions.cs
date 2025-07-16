using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Metroit.Mvvm.WinForms.Extensions
{
    /// <summary>
    /// プロパティのバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class PropertyBindExtensions
    {
        /// <summary>
        /// プロパティをバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="targetPropertyExpression">バインド対象とするプロパティの式木。</param>
        /// <param name="bindPropertyExpression">バインドする値の式木。</param>
        public static void Bind<T, U>(Expression<Func<T>> targetPropertyExpression, Expression<Func<U>> bindPropertyExpression)
        {
            Bind(targetPropertyExpression, bindPropertyExpression, false, DataSourceUpdateMode.OnPropertyChanged,
                string.Empty, null);
        }

        /// <summary>
        /// プロパティをバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="targetPropertyExpression">バインド対象とするプロパティの式木。</param>
        /// <param name="bindPropertyExpression">バインドする値の式木。</param>
        /// <param name="formattingEnabled">表示されるデータの書式を指定する場合は true。それ以外の場合は false。既定は false です。</param>
        public static void Bind<T, U>(Expression<Func<T>> targetPropertyExpression, Expression<Func<U>> bindPropertyExpression,
            bool formattingEnabled)
        {
            Bind(targetPropertyExpression, bindPropertyExpression, formattingEnabled, DataSourceUpdateMode.OnPropertyChanged,
                string.Empty, null);
        }

        /// <summary>
        /// プロパティをバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="targetPropertyExpression">バインド対象とするプロパティの式木。</param>
        /// <param name="bindPropertyExpression">バインドする値の式木。</param>
        /// <param name="formattingEnabled">表示されるデータの書式を指定する場合は true。それ以外の場合は false。既定は false です。</param>
        /// <param name="dataSourceUpdateMode"><see cref="DataSourceUpdateMode"/> 値のいずれか 1 つ。既定は <see cref="DataSourceUpdateMode.OnPropertyChanged"/> です。</param>
        public static void Bind<T, U>(Expression<Func<T>> targetPropertyExpression, Expression<Func<U>> bindPropertyExpression,
            bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode)
        {
            Bind(targetPropertyExpression, bindPropertyExpression, formattingEnabled, dataSourceUpdateMode,
                string.Empty, null);
        }

        /// <summary>
        /// プロパティをバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="targetPropertyExpression">バインド対象とするプロパティの式木。</param>
        /// <param name="bindPropertyExpression">バインドする値の式木。</param>
        /// <param name="formattingEnabled">表示されるデータの書式を指定する場合は true。それ以外の場合は false。既定は false です。</param>
        /// <param name="dataSourceUpdateMode"><see cref="DataSourceUpdateMode"/> 値のいずれか 1 つ。既定は <see cref="DataSourceUpdateMode.OnPropertyChanged"/> です。</param>
        /// <param name="formatString">値の表示方法を示す 1 つ以上の書式指定子文字。既定は <see cref="string.Empty"/> です。</param>
        public static void Bind<T, U>(Expression<Func<T>> targetPropertyExpression, Expression<Func<U>> bindPropertyExpression,
            bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, string formatString)
        {
            Bind(targetPropertyExpression, bindPropertyExpression, formattingEnabled, dataSourceUpdateMode,
                formatString, null);
        }

        /// <summary>
        /// プロパティをバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="targetPropertyExpression">バインド対象とするプロパティの式木。</param>
        /// <param name="bindPropertyExpression">バインドする値の式木。</param>
        /// <param name="formattingEnabled">表示されるデータの書式を指定する場合は true。それ以外の場合は false。既定は false です。</param>
        /// <param name="dataSourceUpdateMode"><see cref="DataSourceUpdateMode"/> 値のいずれか 1 つ。既定は <see cref="DataSourceUpdateMode.OnPropertyChanged"/> です。</param>
        /// <param name="formatString">値の表示方法を示す 1 つ以上の書式指定子文字。既定は <see cref="string.Empty"/> です。</param>
        /// <param name="formatInfo">既定の書式指定動作をオーバーライドする <see cref="IFormatProvider"/> の実装。既定は null です。</param>
        public static void Bind<T, U>(Expression<Func<T>> targetPropertyExpression, Expression<Func<U>> bindPropertyExpression,
            bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, string formatString,
            IFormatProvider formatInfo)
        {
            (object Control, string PropertyName) ResolveLambda<V>(Expression<Func<V>> expression)
            {
                var lambda = expression as LambdaExpression;
                if (lambda == null)
                {
                    throw new ArgumentException("There is an error in your lambda expression.");
                }
                var property = lambda.Body as MemberExpression;
                if (property == null)
                {
                    throw new ArgumentException("It was not possible to obtain a member from a lambda expression.");
                }
                var parent = property.Expression;
                return (Expression.Lambda(parent).Compile().DynamicInvoke(), property.Member.Name);
            }

            var targetControlInfo = ResolveLambda(targetPropertyExpression);
            var expressionObjectInfo = ResolveLambda(bindPropertyExpression);

            var control = targetControlInfo.Control as Control;
            if (control == null)
            {
                throw new ArgumentException("The object resulting from a lambda expression is not a Control object.");
            }

            var binding = new Binding(targetControlInfo.PropertyName, expressionObjectInfo.Control, expressionObjectInfo.PropertyName);
            binding.FormattingEnabled = formattingEnabled;
            binding.FormatString = formatString;
            binding.DataSourceUpdateMode = dataSourceUpdateMode;
            binding.FormatInfo = formatInfo;

            control.DataBindings.Add(binding);
        }
    }
}
