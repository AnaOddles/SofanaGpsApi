using SofanaGPSApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SofanaGPSApi.Services
{
    public class LocationService : ILocationService
    {
        private readonly IMongoCollection<Location> _locations;

        //Constructor - use MongoDb Driver to grab our locations collection from database
        public LocationService(ISofanaGPSDatabaseSettings dbSettings)
        { 
            //MongoDb Driver used to perform CRUD operations
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _locations = database.GetCollection<Location>(dbSettings.SofanaGPSCollectionName);
        }

        //Gets all the locations that are stored in database
        public async Task<List<Location>> Get()
        {
            return await _locations.Find(location => true).SortByDescending(location => location.Id).ToListAsync();
        }

        //Gets the specific location with the given id
        public async Task<List<Location>> Get(string id) =>
            await _locations.Find<Location>(location => location.Id == id).Limit(1).ToListAsync();

        //Get all the specific location with a given cartId 
        public async Task<List<Location>> GetAllWithCartId(int cartId) =>
             await _locations.Find(location => location.cartId == cartId).SortByDescending(location => location.Id).ToListAsync();

        //Grab the last gps coordinate in the database for a cartId
        public async Task<List<Location>> GetLastWithCartId(int cartId)
        {
            return await _locations.Find(location => location.cartId == cartId)
             .SortByDescending(location => location.Id)
             .Limit(1)
             .ToListAsync();
        }

        //Grab the last gps coordinates in the database for all carts
        public async Task<List<Location>> GetLastCoordinates()
        {
            //Use location service for both golf carts
            List<Location> locations = await this.GetLastWithCartId(0);
            locations.AddRange(await this.GetLastWithCartId(1));
            return locations;
        }

        //Grab the last single gps coordinate in the databse
        public async Task<List<Location>> GetLast()
        {
           return await _locations.Find(location => true).SortByDescending(location => location.Id).Limit(1).ToListAsync();
        }
         
        //Creates a new row in database using the provided location information
        public async Task<List<Location>> Create(Location location)
        {
            List<Location> locations = new List<Location>
            {
                location
            };
            await _locations.InsertOneAsync(location);
            return locations;
        }

        //Updates the row with given id in database using the provided location information
        public void Update(string id, Location locationIn) =>
            _locations.ReplaceOne(location => location.Id == id, locationIn);

        //Deletes a row from database using the given location information
        public void Remove(Location locationIn) =>
             _locations.DeleteOne(location => location.Id == locationIn.Id);

        //Deletes a row from database using the given location id
        public void Remove(string id) =>
           _locations.DeleteOne(location => location.Id == id);

        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
