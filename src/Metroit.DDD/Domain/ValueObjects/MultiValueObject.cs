using Metroit.DDD.Domain.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// 複数の値の値オブジェクトの基底操作を提供します。
    /// </summary>
    /// <remarks>
    /// public ではないプロパティまたはフィールドには値が設定されますが、検証は行われません。
    /// </remarks>
    public abstract class MultiValueObject : ValueObject
    {
        /// <summary>
        /// 新しいインスタンスを生成します。値は即時検証されます。
        /// </summary>
        /// <param name="values">値のコレクション。</param>
        protected MultiValueObject(params object[] values) : this(true, values) { }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="validate">即時検証を行うかどうか。</param>
        /// <param name="values">値のコレクション。</param>
        protected MultiValueObject(bool validate, params object[] values)
        {
            AutoFeedMember(values);
            if (validate)
            {
                ValidateObject();
            }
        }


        ///// <summary>
        ///// 検証コンテキスト、およびすべてのプロパティを検証するかどうかを指定する値を使用して、指定されたオブジェクトが有効かどうかを判断します。
        ///// </summary>
        //protected void ValidateObject()
        //{
        //    var r = new List<ValidationResult>();
        //    var b = Validator.TryValidateObject(this, new ValidationContext(this), r, true);

        //    Validator.ValidateObject(this, new ValidationContext(this), true);
        //}

        ///// <summary>
        ///// 検証コンテキスト、検証結果のコレクション、およびすべてのプロパティを検証するかどうかを指定する値を使用して、指定されたオブジェクトが有効かどうかを判断します。
        ///// </summary>
        ///// <param name="result">失敗した各検証を保持するコレクション。</param>
        ///// <returns>オブジェクトが有効な場合は true。それ以外の場合は false を返却します。</returns>
        //protected bool TryValidateObject(out IEnumerable<ValidationResult> result)
        //{
        //    var validationResuts = new List<ValidationResult>();
        //    var r = Validator.TryValidateObject(this, new ValidationContext(this), validationResuts, true);
        //    result = validationResuts;

        //    return r;
        //}

        /// <summary>
        /// <seealso cref="VOFeedOrderAttribute"/> 属性が指定されたプロパティまたはフィールドに対して、値を自動的に設定する。
        /// </summary>
        /// <param name="values">値のコレクション。</param>
        private void AutoFeedMember(params object[] values)
        {
            var orderedMembers = new List<(VOFeedOrderAttribute OrderAttribute, MemberInfo Property)>(
                GetType()
                .GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.SetField)
                .Select(x => (Propperty: x, OrderAttribute: x.GetCustomAttribute<VOFeedOrderAttribute>()))
                .Where(x => x.OrderAttribute != null)
                .Select(x => (x.OrderAttribute, (MemberInfo)x.Propperty))
                .ToList()
                );

            orderedMembers.Sort((x, y) => x.OrderAttribute.Order.CompareTo(y.OrderAttribute.Order));

            values
                .Select((Value, Index) => new { Value, Index })
                .ToList()
                .ForEach(x =>
                {
                    var orderMatchMember = orderedMembers.Where(y => y.OrderAttribute.Order == x.Index).SingleOrDefault();
                    if (orderMatchMember.Property == null)
                    {
                        return;
                    }
                    if (orderMatchMember.Property is PropertyInfo prop)
                    {
                        prop.SetValue(this, x.Value);
                    }
                    else if (orderMatchMember.Property is FieldInfo field)
                    {
                        field.SetValue(this, x.Value);
                    }
                });
        }
    }
}
