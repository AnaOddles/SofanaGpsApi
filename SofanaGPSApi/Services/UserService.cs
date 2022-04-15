using MongoDB.Driver;
using SofanaGPSApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SofanaGPSApi.Services
{
    /// <summary>
    /// User Service class utilized for api authentication
    /// </summary>
    public class UserService : IUserService
    {
        public readonly IMongoCollection<User> _users;

        /// <summary>
        /// Constructor - use MongoDb Driver to grab our user collection from database
        /// </summary>
        /// <param name="dbSettings"></param>
        public UserService(ISofanaGPSDatabaseSettings dbSettings)
        {
            //MongoDb Driver used to perform CRUD operations
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _users = database.GetCollection<User>(dbSettings.SofanaGPSUserCollectionName);
        }

        /// <summary>
        /// Grabs count of user from passed in credentials - can be used for authentication service for admin
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>long</returns>
        public long Get(string username, string password) =>
            _users.Find<User>(user => user.username == username && user.password == password).CountDocuments();

        /// <summary>
        /// Gets all the users that are stored in database
        /// </summary>
        /// <returns>List<User></returns>
        public List<User> Get() =>
           _users.Find(user => true).SortByDescending(user => user.Id).ToList();

        /// <summary>
        /// Get a user with a username pass in
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User</returns>
        public User Get(string username) =>
            _users.Find<User>(user => user.username == username).FirstOrDefault();

    }
}
