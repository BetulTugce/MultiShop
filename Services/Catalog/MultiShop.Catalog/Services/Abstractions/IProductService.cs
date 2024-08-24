using MultiShop.Catalog.Dtos.ProductDtos;

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
	}
}
