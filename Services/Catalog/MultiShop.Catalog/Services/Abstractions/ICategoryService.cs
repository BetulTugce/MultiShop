using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Services.Abstractions
{
	public interface ICategoryService
	{
		Task<List<ResultCategoryDto>> GetAllCategoriesAsync();
		Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
		Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
		Task DeleteCategoryAsync(string id);
		Task<GetByIdCategoryDto> GetCategoryByIdAsync(string id);
        Task<List<FeaturedCategoryDto>> GetCategoriesWithProductCountAsync();
    }
}
