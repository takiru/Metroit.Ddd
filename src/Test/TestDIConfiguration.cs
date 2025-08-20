using Metroit.Ddd.ContentRoot;

namespace Test
{
    class TestDIConfiguration : DIConfigration
    {
        protected override void OnServiceConfiguring(DIServiceConfigurationBuilder builder)
        {
            builder.ApplyConfiguration(new TestDIViewConfiguration());
        }
    }
}
