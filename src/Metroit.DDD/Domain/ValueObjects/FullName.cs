namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// 氏名を提供します。
    /// </summary>
    public class FullName
    {
        /// <summary>
        /// 氏名を取得します。
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="fullName">氏名。</param>
        public FullName(string fullName)
        {
            Value = fullName;
        }

        /// <summary>
        /// 氏名を文字列として返します。
        /// </summary>
        /// <returns>氏名。</returns>
        public override string ToString() => Value;
    }
}
