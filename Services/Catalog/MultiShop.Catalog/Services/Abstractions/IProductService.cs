using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Services.Abstractions
{
	public interface IProductService
	{
		Task<List<ResultProductDto>> GetAllProductsAsync();
		Task CreateProductAsync(CreateProductDto createProductDto);
		Task UpdateProductAsync(UpdateProductDto updateProductDto);
		Task DeleteProductAsync(string id);
		Task<GetByIdProductDto> GetProductByIdAsync(string id);

		Task<List<ResultProductDto>> GetProductsByPageAsync(int page, int size);
		Task<List<ResultProductDto>> GetProductsByCategoryAndPageAsync(string categoryId, int page, int size);
        Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync();
        Task<List<ResultProductDto>> GetFeaturedProductsAsync();

        Task<string> GetProductCoverImageByIdAsync(string id);
    }
}
