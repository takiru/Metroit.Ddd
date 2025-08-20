using Metroit.Ddd.ContentRoot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    class TestDIViewConfiguration : IDIServiceConfiguration
    {
        public void Configure(DIConfigurationServiceBuilder builder)
        {
            //builder.Services.AddDbContext<MyDbContext>((serviceProvider, options) =>
            //{
            //    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //    options.UseSqlServer(builder.Context.Configuration.GetConnectionString("MaestroDbContext"));
            //    options.UseLoggerFactory(loggerFactory);
            //}, ServiceLifetime.Scoped, ServiceLifetime.Scoped);


            ////builder.Services.AddSingleton(new Form2());
            //builder.Services.AddTransient(typeof(Form1));
            //builder.Services.AddTransient(typeof(Form2));
            //builder.Services.AddTransient(typeof(TestDiApp));
            //builder.Services.AddScoped(typeof(TestDiAppDependDbContext));
            ////builder.Services.RegisterFromJsonConfig(builder.Context.Configuration, )
        }
    }
}
