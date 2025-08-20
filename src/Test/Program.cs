using Metroit.Ddd.EntityFrameworkCore.SqlServer;
using Metroit.Ddd.NLog;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows.Forms;

namespace Test
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());



            var di = new TestDIConfiguration();
            di.JsonStreams = new[] { new FileStream("di.json", FileMode.Open, FileAccess.Read, FileShare.Read) };
            di.LoggerConfigurations = new() { { "NLog", new DINLogConfiguration() } };
            di.DbContextConfigurations = new() { { "SqlServer", new DIMsSqlConfiguration() } };
            di.Configure();
            var mainForm = di.Host.Services.GetRequiredService<Form1>();
            Application.Run(mainForm);
        }
    }
}