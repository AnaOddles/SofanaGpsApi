﻿using SofanaGPSApi.Models;
using MongoDB.Driver;
using System.Collections.Generic; 
using System.Linq;

namespace SofanaGPSApi.Services
{
    public class LocationService
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
        public List<Location> Get() =>
               _locations.Find(location => true).ToList();

        //Gets the specific location with the given id
        public Location Get(string id) =>
            _locations.Find<Location>(location => location.Id == id).FirstOrDefault();

        //Creates a new row in database using the provided location information
        public Location Create(Location location)
        {
            _locations.InsertOne(location);
            return location;
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
                
    }
}
