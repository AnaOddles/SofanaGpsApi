namespace SofanaGPSApi.Models
{
    public class SofanaGPSDatabaseSettings : ISofanaGPSDatabaseSettings
    {
        public string SofanaGPSCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface ISofanaGPSDatabaseSettings
    {
        string SofanaGPSCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
