using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Repositories.Abstractions
{
	public interface ICategoryRepository : IGenericRepository<Category>
	{
		Task<List<Category>> GetCategoriesWithProductCountAsync();
	}
}
