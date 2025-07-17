using Metroit.DDD.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Metroit.DDD.Domain.Annotations
{
    /// <summary>
    /// ValueObject クラスに指定された場合、または ValueObject クラス内のプロパティに指定された場合に、値の最小長と最大長を検証する属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class VOLengthAttribute : ValidationAttribute
    {
        /// <summary>
        /// 許容される最大長を取得します。
        /// </summary>
        public int MaximumLength { get; }

        /// <summary>
        /// 許容される最小長を取得します。
        /// </summary>
        public int MinimumLength { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="maximumLength">許容される最小長。</param>
        /// <param name="maximumLength">許容される最大長。</param>
        public VOLengthAttribute(int minimumLength, int maximumLength) : base()
        {
            MinimumLength = minimumLength;
            MaximumLength = maximumLength;
        }

        /// <summary>
        /// エラーメッセージをフォーマットします。
        /// </summary>
        /// <param name="name">書式設定されたメッセージに含める名前。</param>
        /// <returns>書式設定されたエラー メッセージ。</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, new object[]
            {
                name,
                MinimumLength,
                MaximumLength
            });
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
        /// 値の長さが最小長と最大長の範囲内にあるかどうかを検証する。
        /// </summary>
        /// <param name="value">検証値。</param>
        /// <returns>妥当な場合は true, それ以外は false を返却する。</returns>
        private bool IsValidValue(object value)
        {
            CheckLegalLengths();

            int length = value == null ? 0 : ((string)value).Length;
            return value == null || (length >= MinimumLength && length <= MaximumLength);
        }

        /// <summary>
        /// 値の最小長と最大長が適切な値であることを確認する。
        /// </summary>
        /// <exception cref="InvalidOperationException">MaximunLength が 0 未満もしくは MaximunLength が MinimumLength より小さい場合に発生する。</exception>
        private void CheckLegalLengths()
        {
            if (MaximumLength < 0)
            {
                throw new InvalidOperationException("The maximum length must be a nonnegative integer.");
            }

            if (MaximumLength < MinimumLength)
            {
                throw new InvalidOperationException(String.Format("The maximum value '{0}' must be greater than or equal to the minimum value '{1}'.", MaximumLength, MinimumLength));
            }
        }
    }
}
