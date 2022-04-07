using MongoDB.Driver;
using SofanaGPSApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SofanaGPSApi.Services
{
    public class UserService
    {
        public readonly IMongoCollection<User> _users;

        //Constructor - use MongoDb Driver to grab our user collection from database
        public UserService(ISofanaGPSDatabaseSettings dbSettings)
        {
            //MongoDb Driver used to perform CRUD operations
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _users = database.GetCollection<User>(dbSettings.SofanaGPSUserCollectionName);
        }

        //Grabs count of user from passed in credentials - can be used for authentication service for admin
        public long Get(string username, string password) =>
            _users.Find<User>(user => user.username == username && user.password == password).CountDocuments();

        //Gets all the users that are stored in database
        public List<User> Get() =>
           _users.Find(user => true).SortByDescending(user => user.Id).ToList();

        //Get a user with a username pass in
        public User Get(string username) =>
            _users.Find<User>(user => user.username == username).FirstOrDefault();

    }
}
