using Metroit.Ddd.ContentRoot;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    class TestDIViewConfiguration : IDITypeConfiguration
    {
        public void Configure(DIConfigurationServiceBuilder builder)
        {
            builder.Services.AddSingleton(new Form2());
        }
    }
}
