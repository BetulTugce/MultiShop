using AutoMapper;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Repositories.Concrete;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Services.Concrete
{
	public class ProductDetailService : IProductDetailService
	{
		private readonly IProductDetailRepository _productDetailRepository;

		private readonly IMapper _mapper;

		public ProductDetailService(IProductDetailRepository productDetailRepository, IMapper mapper)
		{
			_productDetailRepository = productDetailRepository;
			_mapper = mapper;
		}

		public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
		{
			var productDetail = _mapper.Map<ProductDetail>(createProductDetailDto);
			await _productDetailRepository.CreateAsync(productDetail);
		}

		public async Task DeleteProductDetailAsync(string id)
		{
			await _productDetailRepository.DeleteAsync(id);
		}

		public async Task<List<ResultProductDetailDto>> GetAllProductDetailsAsync()
		{
			var productDetails = await _productDetailRepository.GetAllAsync();
			return _mapper.Map<List<ResultProductDetailDto>>(productDetails);
		}

		public async Task<GetByIdProductDetailDto> GetProductDetailByIdAsync(string id)
		{
			var productDetail = await _productDetailRepository.GetByIdAsync(id);
			return _mapper.Map<GetByIdProductDetailDto>(productDetail);
		}

        public async Task<GetByIdProductDetailDto> GetProductDetailByProductIdAsync(string productId)
        {
            var productDetail = await _productDetailRepository.GetProductDetailByProductIdAsync($"{productId}");
			return _mapper.Map<GetByIdProductDetailDto>(productDetail);
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
		{
			var productDetail = _mapper.Map<ProductDetail>(updateProductDetailDto);
			await _productDetailRepository.UpdateAsync(productDetail);
		}
	}
}
