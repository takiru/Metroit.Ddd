using Metroit.DDD.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Metroit.DDD.Domain.Annotations
{
    /// <summary>
    /// ValueObject クラスに指定された場合、または ValueObject クラス内のプロパティに指定された場合に、値の範囲を検証する属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class VORangeAttribute : RangeAttribute
    {
        /// <summary>
        /// 最小許容値および最大許容値の書式を指定します。
        /// </summary>
        public string Format { get; } = default;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="minimum">データ フィールド値の最小許容値。</param>
        /// <param name="maximum">データ フィールド値の最大許容値</param>
        public VORangeAttribute(double minimum, double maximum) : base(minimum, maximum)
        {

        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="minimum">データ フィールド値の最小許容値。</param>
        /// <param name="maximum">データ フィールド値の最大許容値</param>
        /// <param name="format">最小許容値および最大許容値の書式。</param>
        public VORangeAttribute(double minimum, double maximum, string format) : base(minimum, maximum)
        {
            Format = format;
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="minimum">データ フィールド値の最小許容値。</param>
        /// <param name="maximum">データ フィールド値の最大許容値</param>
        public VORangeAttribute(int minimum, int maximum) : base(minimum, maximum)
        {

        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="minimum">データ フィールド値の最小許容値。</param>
        /// <param name="maximum">データ フィールド値の最大許容値</param>
        /// <param name="format">最小許容値および最大許容値の書式。</param>
        public VORangeAttribute(int minimum, int maximum, string format) : base(minimum, maximum)
        {
            Format = format;
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="length">テストするオブジェクトの型。</param>
        /// <param name="minimum">データ フィールド値の最小許容値。</param>
        /// <param name="maximum">データ フィールド値の最大許容値</param>
        public VORangeAttribute(Type type, string minimum, string maximum) : base(type, minimum, maximum)
        {

        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="type">テストするオブジェクトの型。</param>
        /// <param name="minimum">データ フィールド値の最小許容値。</param>
        /// <param name="maximum">データ フィールド値の最大許容値</param>
        /// <param name="format">最小許容値および最大許容値の書式。</param>
        public VORangeAttribute(Type type, string minimum, string maximum, string format) : base(type, minimum, maximum)
        {
            Format = format;
        }

        /// <summary>
        /// エラーメッセージをフォーマットします。
        /// </summary>
        /// <param name="name">書式設定されたメッセージに含める名前。</param>
        /// <returns>書式設定されたエラー メッセージ。</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString, new[]
            {
                name,
                string.Format("{0:" + Format + "}", Minimum),
                string.Format("{0:" + Format + "}", Maximum)
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
