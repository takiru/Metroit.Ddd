using Reactive.Bindings;
using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Metroit.MVVM.WinForms.Extensions
{
    /// <summary>
    /// ReactiveProperty による値、状態のバインドを行う拡張メソッドを提供します。
    /// </summary>
    public static class PropertyBindExtensions
    {
        /// <summary>
        /// 値、状態をバインドします。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="bindingProperty">バインドするプロパティの Expression。</param>
        /// <param name="bindExpression">バインドする値のExpression。</param>
        public static void Bind<T, U>(Expression<Func<T>> bindingProperty, Expression<Func<U>> bindExpression)
        {
            (object Control, string PropertyName) ResolveLambda<V>(Expression<Func<V>> expression)
            {
                var lambda = expression as LambdaExpression;
                if (lambda == null) throw new ArgumentException("There is an error in your lambda expression.");
                var property = lambda.Body as MemberExpression;
                if (property == null) throw new ArgumentException("It was not possible to obtain a member from a lambda expression.");
                var parent = property.Expression;
                return (Expression.Lambda(parent).Compile().DynamicInvoke(), property.Member.Name);
            }
            var tuple1 = ResolveLambda(bindingProperty);
            var tuple2 = ResolveLambda(bindExpression);
            var control = tuple1.Control as Control;
            //var control2 = tuple1.Item1 as AutoCompleteBox;
            //if (control == null && control2 == null) throw new ArgumentException();

            //if (control != null)
            //{
            //    // 既に設定済みのバインドを解除する
            //    var binding = control.DataBindings[tuple1.Item2];
            //    if (binding != null)
            //    {
            //        control.DataBindings.Remove(binding);
            //    }

            //    // NOTE: UIのプロパティがNullableの場合、正しくデータバインドできないため、formattingEnabled を true とする。
            //    control.DataBindings.Add(new Binding(tuple1.Item2, tuple2.Item1, tuple2.Item2, true, DataSourceUpdateMode.OnPropertyChanged));
            //}
            //else
            //{
            //    // 既に設定済みのバインドを解除する
            //    var binding = control2.DataBindings[tuple1.Item2];
            //    if (binding != null)
            //    {
            //        control2.DataBindings.Remove(binding);
            //    }

            //    // NOTE: UIのプロパティがNullableの場合、正しくデータバインドできないため、formattingEnabled を true とする。
            //    control2.DataBindings.Add(new Binding(tuple1.Item2, tuple2.Item1, tuple2.Item2, true, DataSourceUpdateMode.OnPropertyChanged));
            //}

            if (control == null) throw new ArgumentException("The object resulting from a lambda expression is not a Control object.");

            // 既に設定済みのバインドは強制的に解除する
            var binding = control.DataBindings[tuple1.PropertyName];
            if (binding != null)
            {
                control.DataBindings.Remove(binding);
            }

            // NOTE: UIにバインドされるプロパティがNullableの場合、正しくデータバインドできないため、formattingEnabled を true とする。
            control.DataBindings.Add(new Binding(tuple1.PropertyName, tuple2.Control, tuple2.PropertyName, true, DataSourceUpdateMode.OnPropertyChanged));
        }

        /// <summary>
        /// ラベルの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label">ラベルオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this Label label, Expression<Func<T>> expression)
        {
            Bind(() => label.Text, expression);
        }

        ///// <summary>
        ///// 数値ラベルの値バインドを行います。
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="label">ラベルオブジェクト。</param>
        ///// <param name="expression">値のExpression。</param>
        //public static void Bind<T>(this MetNumericLabel label, Expression<Func<T>> expression)
        //{
        //    Bind(() => label.Value, expression);
        //}

        /// <summary>
        /// テキストボックスの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="textBox">テキストボックスオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this TextBoxBase textBox, Expression<Func<T>> expression)
        {
            Bind(() => textBox.Text, expression);
        }

        ///// <summary>
        ///// 数値テキストボックスの値バインドを行います。
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="textBox">テキストボックスオブジェクト。</param>
        ///// <param name="expression">値のExpression。</param>
        //public static void Bind<T>(this MetNumericTextBox textBox, Expression<Func<T>> expression)
        //{
        //    Bind(() => textBox.Value, expression);
        //}

        /// <summary>
        /// 日付ピッカーの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dateTimePicker">日付ピッカーオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this DateTimePicker dateTimePicker, Expression<Func<T>> expression)
        {
            Bind(() => dateTimePicker.Value, expression);
        }

        /// <summary>
        /// ラジオボタンの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="radioButton">ラジオボタンオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this RadioButton radioButton, Expression<Func<T>> expression)
        {
            Bind(() => radioButton.Checked, expression);
        }

        /// <summary>
        /// コンボボックスの値バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comboBox">コンボボックスオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void Bind<T>(this ComboBox comboBox, Expression<Func<T>> expression)
        {
            Bind(() => comboBox.SelectedItem, expression);
        }

        /// <summary>
        /// コンボボックスのデータソースバインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listControl">リストコントロールオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        /// <param name="valueMember">値のメンバ名。</param>
        /// <param name="displayMenber">表示値のメンバ名。</param>
        public static void BindDataSource<T>(this ListControl listControl, Expression<Func<T>> expression, string valueMember, string displayMenber)
        {
            Bind(() => listControl.DataSource, expression);
            listControl.ValueMember = valueMember;
            listControl.DisplayMember = displayMenber;
        }

        /// <summary>
        /// リストコントロールのクリックバインドを行います。
        /// </summary>
        /// <param name="listControl">リストコントロールオブジェクト。</param>
        /// <param name="command">コマンド。</param>
        public static void Bind(this ListControl listControl, ReactiveCommand command)
        {
            command.CanExecuteChanged += (sender, args) => listControl.Enabled = command.CanExecute();
            listControl.Click += (sender, args) => command.Execute();

            // 初期状態を決定
            listControl.Enabled = command.CanExecute();
        }

        /// <summary>
        /// リストコントロールのクリックバインドを行います。
        /// </summary>
        /// <param name="listControl">リストコントロールオブジェクト。</param>
        /// <param name="command">コマンド。</param>
        public static void Bind(this ListControl listControl, AsyncReactiveCommand command)
        {
            command.CanExecuteChanged += (sender, args) => listControl.Enabled = command.CanExecute();
            listControl.Click += (sender, args) => command.Execute();

            // 初期状態を決定
            listControl.Enabled = command.CanExecute();
        }

        /// <summary>
        /// ボタンの実行バインドを行います。
        /// </summary>
        /// <param name="button">ボタンオブジェクト。</param>
        /// <param name="command">コマンド。</param>
        public static void Bind(this Button button, ReactiveCommand command)
        {
            command.CanExecuteChanged += (sender, args) => button.Enabled = command.CanExecute();
            button.Click += (sender, args) => command.Execute();

            // 初期状態を決定
            button.Enabled = command.CanExecute();
        }

        /// <summary>
        /// ボタンの実行バインドを行います。
        /// </summary>
        /// <param name="button">ボタンオブジェクト。</param>
        /// <param name="command">コマンド。</param>
        public static void Bind(this Button button, AsyncReactiveCommand command)
        {
            command.CanExecuteChanged += (sender, args) => button.Enabled = command.CanExecute();
            button.Click += (sender, args) => command.Execute();

            // 初期状態を決定
            button.Enabled = command.CanExecute();
        }

        /// <summary>
        /// コントロールの活性バインドを行います。
        /// </summary>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void BindEnabled<T>(this Control control, Expression<Func<T>> expression)
        {
            Bind(() => control.Enabled, expression);
        }

        /// <summary>
        /// コントロールの表示バインドを行います。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control">コントロールオブジェクト。</param>
        /// <param name="expression">値のExpression。</param>
        public static void BindVisible<T>(this Control control, Expression<Func<T>> expression)
        {
            Bind(() => control.Visible, expression);
        }
    }
}
