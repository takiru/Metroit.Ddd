using System.Text;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// 会社情報を提供します。
    /// </summary>
    public class Company
    {
        /// <summary>
        /// 会社名を取得します。
        /// </summary>
        public string CompanyName { get; }

        /// <summary>
        /// 部署名を取得します。
        /// </summary>
        public string DepartmentName { get; }

        /// <summary>
        /// 担当者を取得します。
        /// </summary>
        public string Responsible { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="companyName">会社名。</param>
        /// <param name="departmentName">部署名。</param>
        /// <param name="responsible">担当者。</param>
        public Company(string companyName, string departmentName, string responsible)
        {
            CompanyName = companyName;
            DepartmentName = departmentName;
            Responsible = responsible;
        }

        /// <summary>
        /// 会社情報を文字列として返します。
        /// </summary>
        /// <returns>会社情報。</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"会社名: {CompanyName}");
            sb.AppendLine($"部署名: {DepartmentName}");
            sb.AppendLine($"担当者: {Responsible}");
            return sb.ToString();
        }
    }
}
