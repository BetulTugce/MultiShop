using MultiShop.Catalog.Dtos.ProductDetailDtos;

namespace MultiShop.Catalog.Services.Abstractions
{
	public interface IProductDetailService
	{
		Task<List<ResultProductDetailDto>> GetAllProductDetailsAsync();
		Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto);
		Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto);
		Task DeleteProductDetailAsync(string id);
		Task<GetByIdProductDetailDto> GetProductDetailByIdAsync(string id);
	}
}
