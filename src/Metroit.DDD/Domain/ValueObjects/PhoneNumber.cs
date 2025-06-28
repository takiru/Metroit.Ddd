using System;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// 電話番号を提供します。
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// 国際番号を取得します。
        /// </summary>
        public string InternationalNumber { get; }

        /// <summary>
        /// 電話番号を取得します。
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="phoneNumber">電話番号。</param>
        public PhoneNumber(string internationalNumber, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(internationalNumber))
            {
                throw new ArgumentException("国際番号は必須です。", nameof(internationalNumber));
            }

            InternationalNumber = internationalNumber;
            if (InternationalNumber.StartsWith("+"))
            {
                InternationalNumber = InternationalNumber.TrimStart('+');
            }

            Value = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber), "電話番号は必須です。");
        }
    }
}
