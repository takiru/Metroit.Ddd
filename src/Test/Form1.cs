using Metroit.DDD.ContentRoot;
using Metroit.DDD.Domain.Annotations;
using Metroit.DDD.Domain.ValueObjects;
using Metroit.MVVM.WinForms.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Test
{
    public partial class Form1 : ViewBase
    {
        private new Form1ViewModel ViewModel => (Form1ViewModel)base.ViewModel;

        public Form1()
        {
            DIConfigration.Configure();

            // launchSettings.json ��ǂݍ��߂�I
            var host = Host.CreateDefaultBuilder(Environment.GetCommandLineArgs()).Build();
            var env = host.Services.GetRequiredService<IHostEnvironment>();

            InitializeComponent();
        }

        public Form1(Form1ViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");


                //var a = new RequiredAttribute();
                //var b = new VORequiredAttribute();
                //var c = new Hoge("a");
                //MessageBox.Show("a");

                //var b = new Hoge("");

                var a = new Fuga(1, "value1", FugaType.Special);
                //var b = new Fuga(123, "value1", FugaType.Special);
                //if (a == b)
                //{
                //    MessageBox.Show("���Ȃ�");
                //}
                MessageBox.Show(a.ToString());
                MessageBox.Show(a.Value2.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().ToString() + "\r\n" + ex.Message);
            }
        }
    }

    [VORequired(ErrorMessage = "{0}�͕K�{�ł��B")]
    //[VORequired(ErrorMessageResourceName = "HogeMessage", ErrorMessageResourceType = typeof(Resource1))]
    //[VOMaxLength(5, ErrorMessage = "{0}��{1}���܂�")]   // null �󕶎��͋��e�����
    //[VOMaxLength(5, ErrorMessageResourceName = "FugaMessage", ErrorMessageResourceType = typeof(Resource1))]
    //[VOMinLength(2, ErrorMessage = "2���ȏ�")]     // null �������e�����
    //[VORegularExpression(@"^[0-9]+$", ErrorMessage = "{0}�͐��l�݂̂œ��͂��Ă��������B")]       // null �󕶎��͋��e�����
    [Display(Name = " DisplayName�Őݒ肵�����O")]
    //[VOLength(2, 5, ErrorMessage = "{0} �� {1}���ȏ�{2}���ȉ��œ��͂��Ă��������B")]
    //[VOStringLength(1, ErrorMessage = "{0}��{1}���܂�")]
    public class Hoge : SingleValueObject<string>
    {
        //public string Value { get; set; }

        //[VORequired(ErrorMessage = "{0}�͕K�{�ł��B")]
        ////[VOMaxLength(2, ErrorMessage = "{0} ��{1} ���܂�")]
        //[Display(Name = " Fuga ��DisplayName")]
        //public string Fuga { get; set; }

        ////[VORange(typeof(DateTime), "2025/01/01 12:23:34", "2025/12/31", "yyyy/MM/dd", ErrorMessage = "{0} �� {1} ���� {2} �܂�")]
        //[Display(Name = "Piyo��DisplayName")]
        //public DateTime Piyo { get; set; }

        //[Display(Name = "Yoka��DisplayName")]
        ////[VOEmailAddress(false, ErrorMessage = "{0}�̓��[���A�h���X�`���œ��͂��Ă��������B")]
        ////[VOMaxLength(2, ErrorMessage = "{0}��{1}���܂�")]
        //public char[] Yoka { get; set; }

        public Hoge(string value) : base(value)
        {
            //Value = value;
            //Fuga = "Fuga";
            //Piyo = new DateTime(2024, 1, 1);
            //Yoka = ['a', 'b', 'c'];

            //Validator.ValidateObject(this, new ValidationContext(this), validateAllProperties: true);
            //ValidateObject();
        }
    }

    public class Fuga : MultiValueObject
    {
        //[VORange(1, 10, ErrorMessage = "{0}��{1}����{2}�͈̔͂œ��͂��Ă��������B")]
        [VORange(1, 200, ErrorMessageResourceName = "FugaValue2Message", ErrorMessageResourceType = typeof(Resource1))]
        [VOFeedOrder(0)]
        [Display(Name = "Value1��DisplayName")]
        public int? Value1 { get; private set; }

        [MinLength(1, ErrorMessage = "10�����ȏ�")]
        [VOFeedOrder(1)]
        public string Value2 { get; private set; }

        [VOFeedOrder(2)]
        public FugaType? FugaType { get; private set; }

        public Fuga(int? value1, string value2, FugaType? fugaType) : base(value1, value2, fugaType)
        {

        }
    }

    public enum FugaType
    {
        Normal,
        Special
    }
}
