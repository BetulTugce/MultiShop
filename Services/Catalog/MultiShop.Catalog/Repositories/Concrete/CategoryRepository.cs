using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(IDatabaseSettings databaseSettings)
			: base(databaseSettings, databaseSettings.CategoryCollectionName)
		{
		}
	}
}
