using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SofanaGPSApi.Models
{
    /// <summary>
    /// Model Entity for User
    /// </summary>
    public class User
    {
        [BsonId] //Donoates primary key
        [BsonRepresentation(BsonType.ObjectId)] // Converts to string for parameters
        
        //Properties w/ getter & setters
        public string Id { get; set; } 

        [BsonElement("username")]
        public string username { get; set; }

        [BsonElement("password")]
        public string password { get; set; }
    }
}
