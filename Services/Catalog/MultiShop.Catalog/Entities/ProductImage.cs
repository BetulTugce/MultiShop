using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entities
{
	public class ProductImage
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ProductImageID { get; set; }
		public List<string> Images { get; set; } = new List<string>();

		public string ProductId { get; set; }
		[BsonIgnore]
		public Product Product { get; set; }
	}

}
