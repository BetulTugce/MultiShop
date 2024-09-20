using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public CategoryRepository(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.CategoryCollectionName)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        }

        public async Task<List<Category>> GetCategoriesWithProductCountAsync()
        {
            var categories = await _collection.Find(_ => true).ToListAsync();
            List<Category> result = new List<Category>();
            foreach (var category in categories)
            {
                // CategoryId'ye göre ürün sayısını alıyor..
                var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, category.Id);
                var productCount = await _productCollection.CountDocumentsAsync(filter);

                // Sonuçları listeye ekliyor..
                result.Add(new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    ImageUrl = category.ImageUrl,
                    ProductCount = productCount
                });
            }

            return result;
        }
    }
}
