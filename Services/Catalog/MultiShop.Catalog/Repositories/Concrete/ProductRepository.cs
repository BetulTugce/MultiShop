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
        private readonly IMongoCollection<ProductDetail> _productDetailCollection;
        private readonly IMongoCollection<ProductImage> _productImageCollection;

        public ProductRepository(IDatabaseSettings databaseSettings) : base(databaseSettings, databaseSettings.ProductCollectionName)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _productDetailCollection = database.GetCollection<ProductDetail>(databaseSettings.ProductDetailCollectionName);
            _productImageCollection = database.GetCollection<ProductImage>(databaseSettings.ProductImageCollectionName);
        }

        public async Task<bool> DeleteProductByProductIdAsync(string productId)
        {
            var pResult = await _collection.DeleteOneAsync(Builders<Product>.Filter.Eq(p => p.Id, productId));

            // Ürün silme başarılıysa diğer ilişkili veriler siliniyor..
            // IsAcknowledged : MongoDbnin isteği kabul edip etmediğini kontrol eder..
            if (pResult.IsAcknowledged && pResult.DeletedCount > 0)
            {
                bool allDeletionsSuccessful = true;

                // ProductDetail siliniyor..
                var dResult = await _productDetailCollection.DeleteOneAsync(Builders<ProductDetail>.Filter.Eq(p => p.ProductId, productId));
                if (!dResult.IsAcknowledged || dResult.DeletedCount == 0)
                {
                    // ProductDetail silme işlemi başarısızsa retry yap..
                    allDeletionsSuccessful = await RetryDeleteAsync(_productDetailCollection, productId, "ProductDetail");
                }

                // ProductImage siliniyor..
                var iResult = await _productImageCollection.DeleteOneAsync(Builders<ProductImage>.Filter.Eq(p => p.ProductId, productId));
                if (!iResult.IsAcknowledged || iResult.DeletedCount == 0)
                {
                    // ProductImage silme işlemi başarısızsa retry yap..
                    //&= bu operator sayesinde productdetail ve productimage için gerçekleştirilen retry işlemlerinden ikisi de true olmadığı sürece false dönecek..
                    allDeletionsSuccessful &= await RetryDeleteAsync(_productImageCollection, productId, "ProductImage");
                }

                return allDeletionsSuccessful;
            }

            // Silme işlemi başarısızsa false döner..
            return false;
        }

        private async Task<bool> RetryDeleteAsync<T>(IMongoCollection<T> collection, string productId, string collectionName)
        {
            int retryCount = 0;
            bool isDeleted = false;

            while (retryCount < 3 && !isDeleted)
            {
                retryCount++;
                var result = await collection.DeleteOneAsync(Builders<T>.Filter.Eq("ProductId", productId));
                if (result.IsAcknowledged && result.DeletedCount > 0)
                {
                    isDeleted = true;
                }

                // Başarısızsa 5 saniye bekler..
                if (!isDeleted)
                {
                    await Task.Delay(5000);
                }
            }

            return isDeleted;
        }

        public async Task<List<Product>> GetFeaturedProductsAsync()
        {
            var filter = Builders<Product>.Filter.Eq(p => p.IsFeatured, true);
            var products = await _collection
                .Find(filter)
                .ToListAsync();
            return products;
        }

        public async Task<string> GetProductCoverImageByIdAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var product = await _collection.Find(filter).FirstOrDefaultAsync();
            return product.ImageUrl;
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
