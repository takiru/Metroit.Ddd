using System.Collections.Generic;

namespace Metroit.Ddd.Domain.ValueObjects
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
        protected SingleValueObject(T value) : this(true, value) { }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="validate">即時検証を行う場合は true, 行わない場合は false を指定します。</param>
        /// <param name="value">値。</param>
        protected SingleValueObject(bool validate, T value)
        {
            Value = OnAcceptingValue(value);

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

        /// <summary>
        /// 値を受け入れるときに発生します。
        /// </summary>
        /// <param name="value">受け入れる値。</param>
        /// <returns>受け入れる値。</returns>
        protected virtual T OnAcceptingValue(T value) => value;
    }
}
