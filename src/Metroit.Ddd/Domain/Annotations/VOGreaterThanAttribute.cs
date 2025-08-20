using System;
using System.ComponentModel.DataAnnotations;

namespace Metroit.Ddd.Domain.Annotations
{
    /// <summary>
    /// ValueObject クラス内のプロパティに指定された場合に、指定したプロパティより値が大きいかの比較を検証する属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class VOGreaterThanAttribute : ValidationAttribute
    {
        /// <summary>
        /// 比較対象とするプロパティ名を取得します。
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// 等しい値を許容するかどうかを取得します。
        /// </summary>
        public bool AcceptEqual { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="propertyName">比較対象とするプロパティ名。</param>
        public VOGreaterThanAttribute(string propertyName) : this(propertyName, false)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="propertyName">比較対象とするプロパティ名。</param>
        /// <param name="acceptEqual">等しい値を許容する場合は true, 許容しない場合は false を指定します。</param>
        public VOGreaterThanAttribute(string propertyName, bool acceptEqual) : base()
        {
            PropertyName = propertyName;
            AcceptEqual = acceptEqual;
        }

        /// <summary>
        /// 指定したプロパティより値が大きいかの比較を検証します。
        /// </summary>
        /// <param name="value">検証値。</param>
        /// <param name="validationContext">検証値のコンテキスト。</param>
        /// <returns>ValidationResult クラスのインスタンス。</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            var otherValue = instance.GetType().GetProperty(PropertyName).GetValue(instance);

            if (AcceptEqual)
            {
                if (((IComparable)value).CompareTo(otherValue) >= 0)
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                if (((IComparable)value).CompareTo(otherValue) > 0)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName),
                new[] { validationContext.ObjectType.Name });
        }
    }
}
