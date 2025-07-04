using Metroit.DDD.Domain.Annotations;
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
        public T Value { get; protected set; }

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
    }
}
