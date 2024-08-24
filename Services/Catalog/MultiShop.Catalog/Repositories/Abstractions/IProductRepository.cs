using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Repositories.Abstractions
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		Task<List<Product>> GetProductsByPageAsync(int page, int size);
	}
}
