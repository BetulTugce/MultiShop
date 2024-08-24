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
	}
}
