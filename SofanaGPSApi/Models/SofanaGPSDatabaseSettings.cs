namespace SofanaGPSApi.Models
{
    //SofanaGPS' Database Model to pass database credentials implements an Interface of SofanaGPS database settings
    public class SofanaGPSDatabaseSettings : ISofanaGPSDatabaseSettings
    {
        public string SofanaGPSCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    //SofanaGPS' interface for database settings
    public interface ISofanaGPSDatabaseSettings
    {
        string SofanaGPSCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
