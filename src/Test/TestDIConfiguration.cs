using Metroit.Ddd.ContentRoot;

namespace Test
{
    class TestDIConfiguration : DIConfigration
    {
        protected override void OnConfiguringServices(DIConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.ApplyConfiguration(new TestDIViewConfiguration());
        }
    }
}
