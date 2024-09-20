using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Repositories.Abstractions
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		Task<List<Product>> GetProductsByPageAsync(int page, int size);

		Task<List<Product>> GetProductsByCategoryAndPageAsync(string categoryId, int page, int size);

		Task<List<Product>> GetProductsWithCategoryAsync();
		Task<List<Product>> GetFeaturedProductsAsync();
	}
}
