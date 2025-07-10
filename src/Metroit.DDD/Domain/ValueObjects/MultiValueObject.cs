using Metroit.DDD.Domain.Annotations;
using System.Collections.Generic;
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
        private object[] _values;

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
            _values = values;
            if (validate)
            {
                ValidateObject();
            }
        }

        /// <summary>
        /// <seealso cref="VOFeedOrderAttribute"/> 属性が指定されたプロパティまたはフィールドに対して、値を自動的に設定する。
        /// </summary>
        /// <param name="values">値のコレクション。</param>
        private void AutoFeedMember(params object[] values)
        {
            var orderedMembers = new List<(VOFeedOrderAttribute OrderAttribute, MemberInfo Property)>(
                GetType()
                .GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                            BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.SetField)
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
                    var orderMatchMember = orderedMembers
                            .Where(y => y.OrderAttribute.Order == x.Index)
                            .SingleOrDefault();
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

        /// <summary>
        /// 値オブジェクトの文字列表現を返します。
        /// </summary>
        /// <returns>値オブジェクトの文字列を返却します。</returns>
        public override string ToString()
        {
            return string.Join(",", _values
                .Select(x => x?.ToString() ?? string.Empty)
                );
        }
    }
}
