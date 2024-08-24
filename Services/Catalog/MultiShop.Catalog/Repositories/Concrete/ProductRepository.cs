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
    }
}
