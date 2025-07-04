using Metroit.DDD.Domain.Annotations;
using Metroit.DDD.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //var a = new RequiredAttribute();
                //var b = new VORequiredAttribute();
                ////CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                //var c = new Hoge("a");
                //MessageBox.Show("a");

                var a = new Fuga("value1", 123);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().ToString() + "\r\n" + ex.Message);
            }
        }
    }

    [VORequired(ErrorMessage = "{0}は必須です。")]
    //[VORequired(ErrorMessageResourceName = "HogeMessage", ErrorMessageResourceType = typeof(Resource1))]
    //[VOMaxLength(5, ErrorMessage = "{0}は{1}桁まで")]   // null 空文字は許容される
    //[VOMaxLength(5, ErrorMessageResourceName = "FugaMessage", ErrorMessageResourceType = typeof(Resource1))]
    //[VOMinLength(2, ErrorMessage = "2桁以上")]     // null だけ許容される
    //[VORegularExpression(@"^[0-9]+$", ErrorMessage = "{0}は数値のみで入力してください。")]       // null 空文字は許容される
    [Display(Name = " DisplayNameで設定した名前")]
    //[VOLength(2, 5, ErrorMessage = "{0} は {1}桁以上{2}桁以下で入力してください。")]
    //[VOStringLength(1, ErrorMessage = "{0}は{1}桁まで")]
    public class Hoge : SingleValueObject<string>
    {
        //public string Value { get; set; }

        //[VORequired(ErrorMessage = "{0}は必須です。")]
        ////[VOMaxLength(2, ErrorMessage = "{0} は{1} 桁まで")]
        //[Display(Name = " Fuga のDisplayName")]
        //public string Fuga { get; set; }

        ////[VORange(typeof(DateTime), "2025/01/01 12:23:34", "2025/12/31", "yyyy/MM/dd", ErrorMessage = "{0} は {1} から {2} まで")]
        //[Display(Name = "PiyoのDisplayName")]
        //public DateTime Piyo { get; set; }

        //[Display(Name = "YokaのDisplayName")]
        ////[VOEmailAddress(false, ErrorMessage = "{0}はメールアドレス形式で入力してください。")]
        ////[VOMaxLength(2, ErrorMessage = "{0}は{1}桁まで")]
        //public char[] Yoka { get; set; }

        public Hoge(string value) : base(value)
        {
            //Value = value;
            //Fuga = "Fuga";
            //Piyo = new DateTime(2024, 1, 1);
            //Yoka = ['a', 'b', 'c'];

            //Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
        }
    }

    public class Fuga : MultiValueObject
    {
        public string Value1 { get; private set; }

        [VORange(1, 10, ErrorMessage = "{0}は{1}から{2}の範囲で入力してください。")]
        public int Value2 { get; private set; }

        public Fuga(string value1, int value2) : base(value1, value2)
        {

        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return (Value1, Value2).GetHashCode();
        }

        protected override void SetValues(params object[] param)
        {
            Value1 = (string)param[0];
            Value2 = (int)param[1];
        }
    }
}
