using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
	public class Product
	{
		/* [BsonId] : MongoDBde id olarak kullanılacak prop belirtir. Bu attribute kullanılmadığında MongoDB otomatik olarak _id alanı oluşturur.
		[BsonRepresentation(BsonType.ObjectId)] : ObjectId, MongoDB'de default ve benzersiz kimlik türüdür. Bu attribute, C# tarafında string tutulan türleri MongoDB'de ObjectId olarak temsil eder ve MongoDB'den gelen ObjectId değerlerini C# tarafında string olarak kullanmanı sağlar.
		[BsonIgnore] : Bir özelliğin MongoDB işlemlerinde göz ardı edilmesini sağlar. Bu attributeu kullandığımız property veritabanına kaydedilmez ve veritabanından okunmaz.*/
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

		[BsonRepresentation(BsonType.ObjectId)]
		public string CategoryId { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }
    }
}
