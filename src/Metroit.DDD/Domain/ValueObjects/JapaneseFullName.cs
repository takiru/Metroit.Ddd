using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// 日本の氏名を提供します。
    /// </summary>
    public class JapaneseFullName : FullName
    {
        /// <summary>
        /// 姓を取得します。
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// 名を取得します。
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="lastName">姓。</param>
        /// <param name="firstName">名。</param>
        public JapaneseFullName(string lastName, string firstName)
            : base($"{lastName} {firstName}")
        {
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName), "姓は必須です。");
            FirstName = firstName;
        }

        /// <summary>
        /// 日本の氏名を文字列として返します。
        /// </summary>
        /// <returns>日本の氏名。</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                return LastName;
            }

            return $"{LastName} {FirstName}";
        }
    }
}
