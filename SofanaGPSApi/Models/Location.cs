using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SofanaGPSApi.Models
{
    public class Location
    {
        [BsonId] //Denoates primiary key 
        [BsonRepresentation(BsonType.ObjectId)] //Converts to string for parameters 
        public string Id { get; set; }
        public string lon { get; set; }
        public string lat { get; set; }
    }
}
