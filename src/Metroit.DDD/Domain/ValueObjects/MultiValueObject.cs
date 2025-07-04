using System.ComponentModel.DataAnnotations;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// 複数の値の値オブジェクトの基底操作を提供します。
    /// </summary>
    /// <typeparam name="T">ValueObject で管理する値。</typeparam>
    public abstract class MultiValueObject : ValueObject
    {
        private readonly object[] _properties;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">値。</param>
        protected MultiValueObject(params object[] properties)
        {
            _properties = properties;

            SetValues(_properties);

            Validator.ValidateObject(this, new ValidationContext(this), true);
        }

        /// <summary>
        /// 値の設定を行います。
        /// </summary>
        /// <param name="properties">値のコレクション。</param>
        protected abstract void SetValues(params object[] properties);
    }
}
