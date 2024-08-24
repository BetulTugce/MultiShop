using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
        public ProductRepository(IDatabaseSettings databaseSettings) : base(databaseSettings, databaseSettings.ProductCollectionName)
        {
            
        }

		public async Task<List<Product>> GetProductsByPageAsync(int page, int size)
		{
			var products = await _collection
			.Find(Builders<Product>.Filter.Empty)
			.Skip((page - 1) * size)
			.Limit(size)
			.ToListAsync();

			return products;
		}
	}
}
