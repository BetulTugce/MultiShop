using AutoMapper;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Repositories.Concrete;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Services.Concrete
{
	public class ProductImageService : IProductImageService
	{
		private readonly IProductImageRepository _productImageRepository;

		private readonly IMapper _mapper;

		public ProductImageService(IProductImageRepository productImageRepository, IMapper mapper)
		{
			_productImageRepository = productImageRepository;
			_mapper = mapper;
		}

		public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
		{
			var productImage = _mapper.Map<ProductImage>(createProductImageDto);
			await _productImageRepository.CreateAsync(productImage);
		}

		public async Task DeleteProductImageAsync(string id)
		{
			await _productImageRepository.DeleteAsync(id);
		}

		public async Task<List<ResultProductImageDto>> GetAllProductImagesAsync()
		{
			var productImages = await _productImageRepository.GetAllAsync();
			return _mapper.Map<List<ResultProductImageDto>>(productImages);
		}

		public async Task<GetByIdProductImageDto> GetProductImageByIdAsync(string id)
		{
			var productImage = await _productImageRepository.GetByIdAsync(id);
			return _mapper.Map<GetByIdProductImageDto>(productImage);
		}

        public async Task<GetByIdProductImageDto> GetProductImageByProductIdAsync(string productId)
        {
			var productImage = await _productImageRepository.GetProductImageByProductIdAsync(productId);
			return _mapper.Map<GetByIdProductImageDto>(productImage);
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
		{
			var productImage = _mapper.Map<ProductImage>(updateProductImageDto);
			await _productImageRepository.UpdateAsync(productImage);
		}
	}
}
