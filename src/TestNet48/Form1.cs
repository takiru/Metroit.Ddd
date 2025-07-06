using Metroit.DDD.ContentRoot;
using System.Diagnostics;
using System.Windows.Forms;

namespace TestNet48
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DIConfigration.Configure();
        }
    }
}
