using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Metroit.Ddd.Domain.Annotations
{
    /// <summary>
    /// ValueObject クラス内のプロパティに指定された場合に、指定したプロパティより値が小さいかの比較を検証する属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class VOLessThanAttribute : ValidationAttribute
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
        /// メッセージに埋込を行うテキスト配列を取得または設定します。
        /// </summary>
        public string[] EmbeddedTexts { get; set; } = null;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="propertyName">比較対象とするプロパティ名。</param>
        public VOLessThanAttribute(string propertyName) : this(propertyName, false)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="propertyName">比較対象とするプロパティ名。</param>
        /// <param name="acceptEqual">等しい値を許容する場合は true, 許容しない場合は false を指定します。</param>
        public VOLessThanAttribute(string propertyName, bool acceptEqual) : base()
        {
            PropertyName = propertyName;
            AcceptEqual = acceptEqual;
        }

        /// <summary>
        /// 指定したプロパティより値が小さいかの比較を検証します。
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
                if (((IComparable)value).CompareTo(otherValue) <= 0)
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                if (((IComparable)value).CompareTo(otherValue) < 0)
                {
                    return ValidationResult.Success;
                }
            }

            var embeddedTexts = new[] { validationContext.DisplayName }.Concat(EmbeddedTexts ?? Enumerable.Empty<string>()).ToArray();
            return new ValidationResult(FormatErrorMessage(embeddedTexts),
                new[] { validationContext.ObjectType.Name });
        }

        /// <summary>
        /// 文字列を埋め込んだメッセージを取得します。
        /// </summary>
        /// <param name="embeddedTexts">埋込を行う文字列。</param>
        /// <returns>文字列を埋め込んだメッセージ。</returns>
        public virtual string FormatErrorMessage(string[] embeddedTexts)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, embeddedTexts);
        }
    }
}
