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
        /// 新しいインスタンスを生成します。値は即時検証されます。
        /// </summary>
        /// <param name="value">値。</param>
        protected SingleValueObject(T value) : this(value, true) { }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="validate">即時検証を行うかどうか。</param>
        protected SingleValueObject(T value, bool validate)
        {
            Value = value;

            if (validate)
            {
                ValidateObject();
            }
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
