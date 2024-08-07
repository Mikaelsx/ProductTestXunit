using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ApiProducts.Domains
{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("price")]
        public decimal? Price { get; set; }
    }
}
