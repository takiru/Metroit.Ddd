using System.Text.RegularExpressions;
using System;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// 日本の電話番号を提供します。
    /// </summary>
    public class JapanesePhoneNumber : PhoneNumber
    {
        ///// <summary>
        ///// 新しいインスタンスを生成します。
        ///// </summary>
        ///// <param name="phoneNumber">電話番号。</param>
        //public JapanesePhoneNumber(string phoneNumber) : base("81", phoneNumber)
        //{
        //}


        private static readonly Regex DigitsOnlyRegex = new Regex(@"^\d{10,11}$");

        public string RawNumber { get; }

        public JapanesePhoneNumber(string phoneNumber) : base("81", phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("電話番号を入力してください。");
            }

            var normalized = phoneNumber.Replace("-", "").Trim();

            if (!DigitsOnlyRegex.IsMatch(normalized))
            {
                throw new ArgumentException("日本の電話番号として正しい形式ではありません。");
            }

            RawNumber = normalized;
        }

        public JapanesePhoneNumber(string number1, string number2, string number3) : this($"{number1}{number2}{number3}") { }

        public string GetHyphenated()
        {
            return HyphenateJapanesePhoneNumber(RawNumber);
        }

        public override string ToString()
        {
            return GetHyphenated();
        }

        private static string HyphenateJapanesePhoneNumber(string digits)
        {
            // 携帯電話
            if (IsMobileNumber(digits))
            {
                return string.Format("{0}-{1}-{2}", digits.Substring(0, 3), digits.Substring(3, 4), digits.Substring(7, 4));
            }

            // IP電話（050）
            if (digits.StartsWith("050") && digits.Length == 11)
            {
                return string.Format("{0}-{1}-{2}", digits.Substring(0, 3), digits.Substring(3, 4), digits.Substring(7, 4));
            }

            // フリーダイヤル（0120, 0800）やナビダイヤル（0570）
            if ((digits.StartsWith("0120") || digits.StartsWith("0800") || digits.StartsWith("0570")) && digits.Length == 10)
            {
                return string.Format("{0}-{1}-{2}", digits.Substring(0, 4), digits.Substring(4, 3), digits.Substring(7, 3));
            }

            // 固定電話：市外局番の長さを試行（4→3→2）
            string[] areaCodeLengths = { "4", "3", "2" };

            foreach (var lenStr in areaCodeLengths)
            {
                var len = int.Parse(lenStr);
                if (digits.Length > len)
                {
                    var area = digits.Substring(0, len);
                    var remaining = digits.Substring(len);
                    if (remaining.Length == 6 || remaining.Length == 7)
                    {
                        var mid = remaining.Substring(0, remaining.Length / 2);
                        var last = remaining.Substring(remaining.Length / 2);
                        return string.Format("{0}-{1}-{2}", area, mid, last);
                    }
                }
            }

            // フォールバック（予備対応）
            if (digits.Length == 10)
            {
                return string.Format("{0}-{1}-{2}", digits.Substring(0, 3), digits.Substring(3, 3), digits.Substring(6));
            }
            else if (digits.Length == 11)
            {
                return string.Format("{0}-{1}-{2}", digits.Substring(0, 3), digits.Substring(3, 4), digits.Substring(7));
            }

            // 未対応形式
            return digits;
        }

        /// <summary>
        /// 携帯電話番号かどうかを判定します。
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool IsMobileNumber(string number)
        {
            if (number.StartsWith("070"))
            {
                return true;
            }

            if (number.StartsWith("080"))
            {
                return true;
            }

            if (number.StartsWith("090"))
            {
                return true;
            }

            return false;
        }

        private static bool IsIPNumber(string number)
        {
            return number.StartsWith("050");
        }
}
