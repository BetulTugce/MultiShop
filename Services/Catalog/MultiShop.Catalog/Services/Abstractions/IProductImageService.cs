using MultiShop.Catalog.Dtos.ProductImageDtos;

namespace MultiShop.Catalog.Services.Abstractions
{
	public interface IProductImageService
	{
		Task<List<ResultProductImageDto>> GetAllProductImagesAsync();
		Task CreateProductImageAsync(CreateProductImageDto createProductImageDto);
		Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
		Task DeleteProductImageAsync(string id);
		Task<GetByIdProductImageDto> GetProductImageByIdAsync(string id);
	}
}
