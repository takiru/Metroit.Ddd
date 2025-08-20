namespace Metroit.Ddd.Domain.ValueObjects
{
    /// <summary>
    /// 単一の値オブジェクトのインターフェースを提供します。
    /// </summary>
    public interface ISingleValueObject
    {
        /// <summary>
        /// 値を取得します。
        /// </summary>
        object Value { get; }
    }
}
