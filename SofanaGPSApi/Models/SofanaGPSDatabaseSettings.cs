namespace SofanaGPSApi.Models
{
    /// <summary>
    /// SofanaGPS' Database Model to pass database credentials
    /// implements an Interface of SofanaGPS database settings
    /// </summary>
    public class SofanaGPSDatabaseSettings : ISofanaGPSDatabaseSettings
    {
        
        public string SofanaGPSCollectionName { get; set; }
        public string SofanaGPSUserCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
    }

    /// <summary>
    /// SofanaGPS' interface for database settings
    /// </summary>
    public interface ISofanaGPSDatabaseSettings
    {
        string SofanaGPSCollectionName { get; set; }
        public string SofanaGPSUserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
