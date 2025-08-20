using Metroit.Ddd.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace Metroit.Ddd.Domain.Annotations
{
    /// <summary>
    /// ValueObject クラスに指定された場合、または ValueObject クラス内のプロパティに指定された場合に、値が必須であることを示す属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class VORequiredAttribute : RequiredAttribute
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public VORequiredAttribute() : base()
        {

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
