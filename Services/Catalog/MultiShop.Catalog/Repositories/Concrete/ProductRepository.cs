using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public ProductRepository(IDatabaseSettings databaseSettings) : base(databaseSettings, databaseSettings.ProductCollectionName)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }

        // Ürünleri sayfa, sayfa boyutu ve kategoriye göre filtreleyerek getirir..
        public async Task<List<Product>> GetProductsByCategoryAndPageAsync(string categoryId, int page, int size)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);

            var products = await _collection
                .Find(filter)
                .Skip((page - 1) * size)
                .Limit(size)
                .ToListAsync();

            return products;
        }

        // Ürünleri sayfa ve sayfa boyutuna göre filtreleyerek getirir..
        public async Task<List<Product>> GetProductsByPageAsync(int page, int size)
        {
            var products = await _collection
            .Find(Builders<Product>.Filter.Empty)
            .Skip((page - 1) * size)
            .Limit(size)
            .ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetProductsWithCategoryAsync()
        {
            var products = await _collection.Find(x => true).ToListAsync();
            foreach (var product in products) 
            {
                product.Category = await _categoryCollection.Find<Category>(x => x.Id == product.CategoryId).FirstAsync();
            }

            return products;
        }
    }
}
