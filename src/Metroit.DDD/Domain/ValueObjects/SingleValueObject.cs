using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// 単一の値オブジェクトの基底操作を提供します。
    /// </summary>
    /// <typeparam name="T">ValueObject で管理する値。</typeparam>
    public abstract class SingleValueObject<T> : ValueObject, ISingleValueObject
    {
        /// <summary>
        /// 値を取得します。
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// 値を取得します。
        /// </summary>
        object ISingleValueObject.Value => Value;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">値。</param>
        protected SingleValueObject(T value)
        {
            Value = value;

            Validator.ValidateObject(this, new ValidationContext(this), true);
        }

        /// <summary>
        /// 比較を行う値のコレクションを返却します。
        /// </summary>
        /// <returns>比較を行う値のコレクション。</returns>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// 値オブジェクトの文字列表現を返します。
        /// </summary>
        /// <returns>値オブジェクトの文字列を返却します。</returns>
        public override string ToString() => Value.ToString();
    }
}
