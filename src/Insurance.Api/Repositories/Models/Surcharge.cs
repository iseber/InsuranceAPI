using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Insurance.Api.Repositories.Models
{
    public class Surcharge
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int ProductTypeId { get; set; }
        public float SurchargeCost { get; set; }
    }
}