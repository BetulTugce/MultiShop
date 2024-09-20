using AutoMapper;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Services.Concrete
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;

		public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}

		public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
		{
			var categories = await _categoryRepository.GetAllAsync();
			return _mapper.Map<List<ResultCategoryDto>>(categories);
		}

		public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
		{
			var category = _mapper.Map<Category>(createCategoryDto);
			await _categoryRepository.CreateAsync(category);
		}

		public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
		{
			var category = _mapper.Map<Category>(updateCategoryDto);
			await _categoryRepository.UpdateAsync(category);
		}

		public async Task DeleteCategoryAsync(string id)
		{
			await _categoryRepository.DeleteAsync(id);
		}

		public async Task<GetByIdCategoryDto> GetCategoryByIdAsync(string id)
		{
			var category = await _categoryRepository.GetByIdAsync(id);
			return _mapper.Map<GetByIdCategoryDto>(category);
		}

        public async Task<List<FeaturedCategoryDto>> GetCategoriesWithProductCountAsync()
        {
            var categories = await _categoryRepository.GetCategoriesWithProductCountAsync();
			return _mapper.Map<List<FeaturedCategoryDto>>(categories);
        }
    }
}
