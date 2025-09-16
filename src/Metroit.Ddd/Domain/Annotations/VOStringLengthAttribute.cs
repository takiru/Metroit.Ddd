using Metroit.Ddd.Domain.ValueObjects;
using Metroit.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Metroit.Ddd.Domain.Annotations
{
    /// <summary>
    /// ValueObject クラスに指定された場合、または ValueObject クラス内のプロパティに指定された場合に、値の最大長を検証する属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class VOStringLengthAttribute : StringLengthAttribute
    {
        /// <summary>
        /// 全角文字を2文字としてカウントするかどうか。
        /// </summary>
        private readonly bool FullWidthCharTwo = false;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="maximumLength">許容される最大長。</param>
        public VOStringLengthAttribute(int maximumLength) : base(maximumLength)
        {

        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="maximumLength">許容される最大長。</param>
        /// <param name="fullWidthCharTwo">全角文字を2文字としてカウントするかどうか。</param>
        public VOStringLengthAttribute(int maximumLength, bool fullWidthCharTwo) : base(maximumLength)
        {
            FullWidthCharTwo = fullWidthCharTwo;
        }

        /// <summary>
        /// 値が有効かどうかを取得します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <returns>有効な場合は <see langword="true"/>, それ以外は <see langword="false"/> を返却します。</returns>
        public override bool IsValid(object value)
        {
            // 全角文字を2文字としてカウントしないなら既存動作通りとする
            if (!FullWidthCharTwo)
            {
                return base.IsValid(value);
            }

            // 値がnullは既存動作通り許容する
            if (value == null)
            {
                return true;
            }

            int length = ((string)value).GetTextCount(FullWidthCharTwo);
            return length >= MinimumLength && length <= MaximumLength;
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
                if (IsValid(innerValue.Value))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName),
                    new[] { validationContext.ObjectType.Name });
            }

            // ValueObject クラス内のプロパティに指定された場合
            if (IsValid(value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName),
                new[] { validationContext.MemberName });
        }
    }
}
