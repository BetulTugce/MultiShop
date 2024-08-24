using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
	public class ProductDetailRepository : GenericRepository<ProductDetail>, IProductDetailRepository
	{
		public ProductDetailRepository(IDatabaseSettings databaseSettings) : base(databaseSettings, databaseSettings.ProductDetailCollectionName)
		{

		}
	}
}
