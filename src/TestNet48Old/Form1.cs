using Metroit.DDD.ContentRoot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestNet48Old
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // NOTE: Visual Studio から実行している場合は、環境変数 DOTNET_ENVIRONMENT を Development に設定する。
            if (Debugger.IsAttached)
            {
                Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");
            }
            DIConfigration.Configure();
        }
    }
}
