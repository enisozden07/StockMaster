using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
    public class StockLevel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ProductId { get; set; }
        public string WarehouseId { get; set; }
        public int Quantity { get; set; }
    }
}
