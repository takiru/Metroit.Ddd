namespace Metroit.DDD.Domain.ValueObjects
{
    public interface ISingleValueObject
    {
        /// <summary>
        /// 値を取得します。
        /// </summary>
        object Value { get; }
    }
}
