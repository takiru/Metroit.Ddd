using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Metroit.Mvvm.ViewModels
{
    /// <summary>
    /// 変更を観察可能なオブジェクトを提供します。
    /// </summary>
    public partial class ChangesObservableObject : ObservableObject
    {
        /// <summary>
        /// 公開しているすべてのプロパティまたはフィールドの既定値。
        /// </summary>
        private Dictionary<string, object> _defaultValues = new Dictionary<string, object>();

        /// <summary>
        /// 変更されたすべてのプロパティまたはフィールドの値。
        /// </summary>
        private Dictionary<string, object> _changedValues = new Dictionary<string, object>();

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public ChangesObservableObject()
        {
            ResetDefaultValues();
            PropertyChanged += ChangesObservableObject_PropertyChanged;
        }

        /// <summary>
        /// 公開しているすべてのプロパティまたはフィールドの既定値をリセットします。
        /// </summary>
        protected void ResetDefaultValues()
        {
            _defaultValues.Clear();
            var properties = GetType().GetProperties(System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField)
                .Where(x => !typeof(IRelayCommand).IsAssignableFrom(x.PropertyType));

            foreach (var property in properties)
            {
                _defaultValues.Add(property.Name, property.GetValue(this));
            }
        }

        /// <summary>
        /// 変更通知が行われたプロパティまたはフィールドを追跡する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangesObservableObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsSomethingValueChanged))
            {
                return;
            }

            var defaultValue = _defaultValues[e.PropertyName];
            var changedValue = GetType().GetProperty(e.PropertyName)?.GetValue(this);

            // 既定値に戻ったときには変更値から除去する
            if (object.Equals(changedValue, defaultValue))
            {
                _changedValues.Remove(e.PropertyName);
                if (_changedValues.Count == 0)
                {
                    IsSomethingValueChanged = false;
                }
                return;
            }

            if (_changedValues.ContainsKey(e.PropertyName))
            {
                _changedValues[e.PropertyName] = changedValue;
            }
            else
            {
                _changedValues.Add(e.PropertyName, changedValue);
            }
            IsSomethingValueChanged = true;
        }

        private bool _isSomethingValueChanged = false;

        /// <summary>
        /// プロパティまたはフィールドの値に変更があったかを取得します。
        /// </summary>
        protected bool IsSomethingValueChanged
        {
            get => _isSomethingValueChanged;
            private set
            {
                if (SetProperty(ref _isSomethingValueChanged, value))
                {
                    OnSomethingValueChanged(value);
                }
            }
        }

        /// <summary>
        /// プロパティまたはフィールドの値に変更があったときに発生します。
        /// </summary>
        /// <param name="value">変更があった場合は true, それ以外は false を返却します。</param>
        protected virtual void OnSomethingValueChanged(bool value) { }

        /// <summary>
        /// 変更されたプロパティまたはフィールドに含まれるかどうかを取得します。
        /// </summary>
        /// <param name="propertyName">変更されたかを取得するプロパティ名またはフィールド名。</param>
        /// <returns>プロパティまたはフィールドが変更されていた場合は true, それ以外は false を返却します。</returns>
        protected bool ContainsChanged(string propertyName)
        {
            return _changedValues.ContainsKey(propertyName);
        }

        /// <summary>
        /// 変更されたプロパティまたはフィールドのコレクションを取得します。
        /// </summary>
        /// <returns>変更されたプロパティまたはフィールドのコレクション。</returns>
        protected IEnumerable<string> GetChangedProperties()
        {
            foreach (var changedValue in _changedValues)
            {
                yield return changedValue.Key;
            }
        }
    }
}
