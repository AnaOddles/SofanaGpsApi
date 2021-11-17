using SofanaGPSApi.Models;
using MongoDB.Driver;
using System.Collections.Generic; 
using System.Linq;

namespace SofanaGPSApi.Services
{
    public class LocationService
    {
        private readonly IMongoCollection<Location> _locations;

        //Constructor - use MongoDb Drivier to grab our locations collection from database
        public LocationService(ISofanaGPSDatabaseSettings dbSettings)
        { 
            //MongoDb Driver used to perform CRUD operations
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _locations = database.GetCollection<Location>(dbSettings.SofanaGPSCollectionName);
        }

        public List<Location> Get() =>
               _locations.Find(location => true).ToList();

        public Location Get(string id) =>
            _locations.Find<Location>(location => location.Id == id).FirstOrDefault();

        public Location Create(Location location)
        {
            _locations.InsertOne(location);
            return location;
        }

        public void Update(string id, Location locationIn) =>
            _locations.ReplaceOne(location => location.Id == id, locationIn);

        public void Remove(Location locationIn) =>
             _locations.DeleteOne(location => location.Id == locationIn.Id);

        public void Remove(string id) =>
           _locations.DeleteOne(location => location.Id == id);
                
    }
}
