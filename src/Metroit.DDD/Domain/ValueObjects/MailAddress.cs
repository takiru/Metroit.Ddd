using System;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// メールアドレスを提供します。
    /// </summary>
    public class MailAddress
    {
        public string Value { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="value">メールアドレス。</param>
        /// <exception cref="ArgumentException">メールアドレスの形式が正しくありません。</exception>
        public MailAddress(string value)
        {
            try
            {
                _ = new System.Net.Mail.MailAddress(value);
            }
            catch (FormatException)
            {
                throw new ArgumentException("メールアドレスの形式が正しくありません。", nameof(value));
            }

            Value = value;
        }
    }
}
