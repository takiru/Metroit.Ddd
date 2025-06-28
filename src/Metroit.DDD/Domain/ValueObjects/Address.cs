using System;
using System.Text.RegularExpressions;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// 住所を提供します。
    /// </summary>
    public class Address
    {
        public string ZipCode { get; }

        public string Prefecture { get; }

        public string City { get; }

        public string Street { get; }

        public string Building { get; }

        public Address(string zipCode, string prefecture, string city, string street, string building = null)
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

            ZipCode = zipCode;
            Prefecture = prefecture;
            City = city;
            Street = street;
            Building = building;
        }
    }
}
