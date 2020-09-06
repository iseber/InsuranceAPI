using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Insurance.Api.Repositories.Models
{
    public class Insurance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public float LowerLimit { get; set; }
        public float UpperLimit { get; set; }
        public float InsuranceCost { get; set; }
    }
}