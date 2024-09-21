using MongoDB.Bson;
using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
	public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
	{
		public ProductImageRepository(IDatabaseSettings databaseSettings) : base(databaseSettings, databaseSettings.ProductImageCollectionName)
		{

		}

        public async Task<ProductImage> GetProductImageByProductIdAsync(string productId)
        {
            return await _collection.Find(Builders<ProductImage>.Filter.Eq(p => p.ProductId, productId.ToString())).FirstOrDefaultAsync();
        }
    }
}
