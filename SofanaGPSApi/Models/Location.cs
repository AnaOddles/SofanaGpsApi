﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SofanaGPSApi.Models
{
    public class Location
    {
        [BsonId] //Denoates primiary key 
        [BsonRepresentation(BsonType.ObjectId)] //Converts to string for parameters 
        public string Id { get; set; }      //location ID with getters and setters
        public string lon { get; set; }     //longitude with getters and setters
        public string lat { get; set; }     //latitude with getters and setters
<<<<<<< HEAD
        public int cartId { get; set; }     //cartId for keeping track of which golf cart locaiton belongs to
        public string dateTime { get; set; } //location dateTime 

=======
        public string dateTime { get; set; }
>>>>>>> e7f5c61d71dbac343dba19476962cfe210f080a5
    }
}
