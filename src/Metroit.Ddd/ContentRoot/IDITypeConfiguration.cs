namespace Metroit.Ddd.ContentRoot
{
    /// <summary>
    /// サービスの構成を提供します。
    /// </summary>
    public interface IDITypeConfiguration
    {
        /// <summary>
        /// サービスを構成します。
        /// </summary>
        /// <param name="builder">構成に使用するビルダー。</param>
        void Configure(DIConfigurationServiceBuilder builder);
    }
}
