using SofanaGPSApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SofanaGPSApi.Services
{
    /// <summary>
    /// Location Service class, inherits ILocationService interface 
    /// </summary>
    public class LocationService : ILocationService
    {
        private readonly IMongoCollection<Location> _locations;

        /// <summary>
        /// Constructor - use MongoDb Driver to grab our locations collection from database
        /// </summary>
        /// <param name="dbSettings"></param>
        public LocationService(ISofanaGPSDatabaseSettings dbSettings)
        { 
            //MongoDb Driver used to perform CRUD operations
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _locations = database.GetCollection<Location>(dbSettings.SofanaGPSCollectionName);
        }

        /// <summary>
        /// Gets all the locations that are stored in database
        /// </summary>
        /// <returns>Task<list<Location>></returns>
        public async Task<List<Location>> Get()
        {
            return await _locations.Find(location => true).SortByDescending(location => location.Id).ToListAsync();
        }

        /// <summary>
        /// Gets all the locations that are stored in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<list<Location>></returns>
        public async Task<List<Location>> Get(string id) =>
            await _locations.Find<Location>(location => location.Id == id).Limit(1).ToListAsync();

        /// <summary>
        /// Get all the specific location with a given cartId 
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>Task<list<Location>></returns>
        public async Task<List<Location>> GetAllWithCartId(int cartId) =>
             await _locations.Find(location => location.cartId == cartId).SortByDescending(location => location.Id).ToListAsync();

        /// <summary>
        /// Grab the last gps coordinate in the database for a cartId
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>Task<list<Location>></returns>
        public async Task<List<Location>> GetLastWithCartId(int cartId)
        {
            return await _locations.Find(location => location.cartId == cartId)
             .SortByDescending(location => location.Id)
             .Limit(1)
             .ToListAsync();
        }

        /// <summary>
        /// Grab the last gps coordinates in the database for all carts
        /// </summary>
        /// <returns>Task<list<Location>></returns>
        public async Task<List<Location>> GetLastCoordinates()
        {
            //Use location service for both golf carts
            List<Location> locations = await this.GetLastWithCartId(0);
            locations.AddRange(await this.GetLastWithCartId(1));
            return locations;
        }

        /// <summary>
        /// Grab the last single gps coordinate in the databse
        /// </summary>
        /// <returns>Task<List<Location>></returns>
        public async Task<List<Location>> GetLast()
        {
           return await _locations.Find(location => true).SortByDescending(location => location.Id).Limit(1).ToListAsync();
        }

        /// <summary>
        /// Creates a new row in database using the provided location information
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Task<List<Location>></returns>
        public async Task<List<Location>> Create(Location location)
        {
            List<Location> locations = new List<Location>
            {
                location
            };
            await _locations.InsertOneAsync(location);
            return locations;
        }
    }
}
