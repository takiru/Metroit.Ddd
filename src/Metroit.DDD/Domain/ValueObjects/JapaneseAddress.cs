using System;
using System.Text.RegularExpressions;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// 日本の住所を提供します。
    /// </summary>
    public class JapaneseAddress : Address
    {
        ///// <summary>
        ///// 郵便番号を取得します。
        ///// </summary>
        //public string ZipCode { get; }

        ///// <summary>
        ///// 都道府県を取得します。
        ///// </summary>
        //public string Prefecture { get; }

        ///// <summary>
        ///// 市区町村を取得します。
        ///// </summary>
        //public string City { get; }

        ///// <summary>
        ///// 番地を取得します。
        ///// </summary>
        //public string Street { get; }

        ///// <summary>
        ///// 建物名を取得します。
        ///// </summary>
        //public string Building { get; }

        public JapaneseAddress(string zipCode, string prefecture, string city, string street, string building = null) : base(zipCode, prefecture, city, street, building)
        {
            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentException("郵便番号は必須です。", nameof(zipCode));
            }
            if (!Regex.IsMatch(zipCode, @"^([0-9]{3}-[0-9]{4}|[0-9]{7})$"))
            {
                throw new ArgumentException("郵便番号の形式が正しくありません。", nameof(zipCode));
            }
            if (string.IsNullOrWhiteSpace(prefecture))
            {
                throw new ArgumentException("都道府県は必須です。", nameof(prefecture));
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("市区町村は必須です。", nameof(city));
            }

            //ZipCode = zipCode;
            //Prefecture = prefecture;
            //City = city;
            //Street = street;
            //Building = building;
        }
    }
}
