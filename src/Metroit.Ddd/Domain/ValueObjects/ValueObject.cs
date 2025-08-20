
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Metroit.Ddd.Domain.ValueObjects
{
    /// <summary>
    /// 値オブジェクトの基底操作を提供します。
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// 両辺の値が同一かどうかを比較します。
        /// </summary>
        /// <param name="left">比較対象の左辺データ。</param>
        /// <param name="right">比較対象の右辺データ。</param>
        /// <returns>同一の場合は true, それ以外の場合は false, 一方が null の場合は false を返却します。</returns>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, right) || left.Equals(right);
        }

        /// <summary>
        /// 両辺の値が同一でないかどうかを比較します。
        /// </summary>
        /// <param name="left">比較対象の左辺データ。</param>
        /// <param name="right">比較対象の右辺データ。</param>
        /// <returns>同一でない場合は true, それ以外の場合は false, 一方が null の場合は false を返却します。</returns>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        /// <summary>
        /// 比較を行う値のコレクションを返却します。
        /// </summary>
        /// <returns>比較を行う値のコレクション。</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// 指定されたデータが同一かどうかを比較します。
        /// </summary>
        /// <param name="obj">比較対象のデータ。</param>
        /// <returns>同一の場合は true, それ以外の場合は false, 比較対象のデータが null の場合は false を返却します。</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// 両辺の値が同一かどうかを比較します。
        /// </summary>
        /// <param name="left">比較対象の左辺データ。</param>
        /// <param name="right">比較対象の右辺データ。</param>
        /// <returns>同一の場合は true, それ以外の場合は false, 一方が null の場合は false を返却します。</returns>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return EqualOperator(left, right);
        }

        /// <summary>
        /// 両辺の値が同一でないかどうかを比較します。
        /// </summary>
        /// <param name="left">比較対象の左辺データ。</param>
        /// <param name="right">比較対象の右辺データ。</param>
        /// <returns>同一でない場合は true, それ以外の場合は false, 一方が null の場合は false を返却します。</returns>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return NotEqualOperator(left, right);
        }

        /// <summary>
        /// 比較を行う値のコレクションを集約したハッシュコードを求めます。
        /// </summary>
        /// <returns>比較を行う値のコレクションを集約したハッシュコード。</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// 検証コンテキスト、およびすべてのプロパティを検証するかどうかを指定する値を使用して、指定されたオブジェクトが有効かどうかを判断します。
        /// </summary>
        protected void ValidateObject()
        {
            Validator.ValidateObject(this, new ValidationContext(this), true);
        }

        /// <summary>
        /// 検証コンテキスト、検証結果のコレクション、およびすべてのプロパティを検証するかどうかを指定する値を使用して、指定されたオブジェクトが有効かどうかを判断します。
        /// </summary>
        /// <param name="result">失敗した各検証を保持するコレクション。</param>
        /// <returns>オブジェクトが有効な場合は true。それ以外の場合は false を返却します。</returns>
        protected bool TryValidateObject(out ICollection<ValidationResult> result)
        {
            var validationResuts = new List<ValidationResult>();
            var r = Validator.TryValidateObject(this, new ValidationContext(this), validationResuts, true);
            result = validationResuts;

            return r;
        }
    }
}
