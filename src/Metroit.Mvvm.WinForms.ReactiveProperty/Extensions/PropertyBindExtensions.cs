using Reactive.Bindings;
using System;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Windows.Input;

namespace Metroit.Mvvm.WinForms.ReactiveProperty.Extensions
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

        /// <summary>
        /// コントロールのテキストをバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindText<T>(this Control control, Expression<Func<T>> expression)
        {
            Bind(() => control.Text, expression);
        }

        /// <summary>
        /// コントロールの活性バインドを行います。
        /// </summary>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindEnabled<T>(this Control control, Expression<Func<T>> expression)
        {
            Bind(() => control.Enabled, expression);
        }

        /// <summary>
        /// コントロールの表示バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindVisible<T>(this Control control, Expression<Func<T>> expression)
        {
            Bind(() => control.Visible, expression);
        }

        /// <summary>
        /// コントロールのクリックをバインドします。
        /// </summary>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="command">コマンド。</param>
        public static void BindClick(this Control control, ReactiveCommand command)
        {
            command.CanExecuteChanged += (sender, args) => control.Enabled = command.CanExecute();
            control.Click += (sender, args) => command.Execute();

            // 初期状態を決定
            control.Enabled = command.CanExecute();
        }

        /// <summary>
        /// コントロールのクリックをバインドします。
        /// 
        /// </summary>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="command">コマンド。</param>
        public static void BindClick(this Control control, AsyncReactiveCommand command)
        {
            command.CanExecuteChanged += (sender, args) => control.Enabled = command.CanExecute();
            control.Click += (sender, args) => command.Execute();

            // 初期状態を決定
            control.Enabled = command.CanExecute();
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// ボタンのクリックをバインドします。
        /// バインドの制御は <paramref name="command"/> に依存します。
        /// </summary>
        /// <param name="button">ボタンオブジェクト。</param>
        /// <param name="command">コマンド。</param>
        public static void BindClickByCommand(this Button button, ICommand command)
        {
            button.Command = command;
        }
#endif

        /// <summary>
        /// 日付ピッカーの値をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dateTimePicker">日付ピッカーオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindValue<T>(this DateTimePicker dateTimePicker, Expression<Func<T>> expression)
        {
            Bind(() => dateTimePicker.Value, expression);
        }

        /// <summary>
        /// チェックボックスのチェック状態をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="checkBox">チェックボックスオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindChecked<T>(this CheckBox checkBox, Expression<Func<T>> expression)
        {
            Bind(() => checkBox.Checked, expression);
        }

        /// <summary>
        /// チェックボックスの状態をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="checkBox">チェックボックスオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindCheckState<T>(this CheckBox checkBox, Expression<Func<T>> expression)
        {
            Bind(() => checkBox.CheckState, expression);
        }

        /// <summary>
        /// ラジオボタンのチェック状態をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="radioButton">ラジオボタンオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindChecked<T>(this RadioButton radioButton, Expression<Func<T>> expression)
        {
            Bind(() => radioButton.Checked, expression);
        }

        /// <summary>
        /// コンボボックスの選択値をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox">コンボボックスオブジェクト。</param>
        /// <param name="expression">バインドする値の式木。</param>
        public static void BindSelectedItem<T>(this ComboBox comboBox, Expression<Func<T>> expression)
        {
            Bind(() => comboBox.SelectedItem, expression);
        }

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
            Bind(() => listControl.DataSource, expression);
            listControl.ValueMember = valueMember;
            listControl.DisplayMember = displayMenber;
        }
    }
}
