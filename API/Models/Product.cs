using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ProductName")]
        public string ProductName { get; set; }

        public string CategoryId { get; set; }
        public string SupplierId { get; set; }
        public decimal Price { get; set; }
    }
}
