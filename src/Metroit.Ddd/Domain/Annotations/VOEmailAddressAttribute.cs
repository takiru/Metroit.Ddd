using Metroit.Ddd.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Metroit.Ddd.Domain.Annotations
{
    /// <summary>
    /// ValueObject クラスに指定された場合、または ValueObject クラス内のプロパティに指定された場合に、値のメールアドレス形式を検証する属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class VOEmailAddressAttribute : DataTypeAttribute
    {
        /// <summary>
        /// null または空文字を許容するかどうかを示す値を取得します。
        /// </summary>
        public bool AcceptNullOrEmpty { get; } = false;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public VOEmailAddressAttribute() : base(DataType.EmailAddress)
        {

        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="acceptNullOrEmpty">null または空文字を許容するかどうかを指定します。</param>
        public VOEmailAddressAttribute(bool acceptNullOrEmpty) : base(DataType.EmailAddress)
        {
            AcceptNullOrEmpty = acceptNullOrEmpty;
        }

        /// <summary>
        /// ValueObject クラスに指定された場合、または ValueObject クラス内のプロパティに指定された場合に、値が有効かどうかを検証します。
        /// </summary>
        /// <param name="value">検証値。</param>
        /// <param name="validationContext">検証値のコンテキスト。</param>
        /// <returns>ValidationResult クラスのインスタンス。</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // ValueObject クラスに指定された場合
            if (value is ISingleValueObject innerValue)
            {
                if (IsValidValue(innerValue.Value))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName),
                    new[] { validationContext.ObjectType.Name });
            }

            // ValueObject クラス内のプロパティに指定された場合
            if (IsValidValue(value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName),
                new[] { validationContext.MemberName });
        }

        /// <summary>
        /// 値がメールアドレスに形式として妥当かどうかを検証する。
        /// </summary>
        /// <param name="value">検証値。</param>
        /// <returns>妥当な場合は true, それ以外は false を返却する。</returns>
        private bool IsValidValue(object value)
        {
            if (AcceptNullOrEmpty)
            {
                if (string.IsNullOrEmpty(value as string))
                {
                    return true;
                }
            }

            try
            {
                _ = new MailAddress(value as string);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
