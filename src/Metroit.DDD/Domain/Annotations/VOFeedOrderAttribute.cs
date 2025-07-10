using Metroit.DDD.Domain.ValueObjects;
using System;

namespace Metroit.DDD.Domain.Annotations
{
    /// <summary>
    /// <see cref="MultiValueObject"/> クラス内のプロパティまたはフィールドに指定された場合に、値を流し込む順序の重みを定義します。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class VOFeedOrderAttribute : Attribute
    {
        /// <summary>
        /// 値を流し込む順序を取得します。
        /// </summary>
        public int Order { get; } = default;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="order">値を流し込む順序。</param>
        public VOFeedOrderAttribute(int order) : base()
        {
            Order = order;
        }
    }
}
