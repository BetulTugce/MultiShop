﻿using AutoMapper;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Repositories.Concrete;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Services.Concrete
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
			_productRepository = productRepository;
			_mapper = mapper;
		}

        public async Task CreateProductAsync(CreateProductDto createProductDto)
		{
			var product = _mapper.Map<Product>(createProductDto);
			await _productRepository.CreateAsync(product);
		}

		public async Task DeleteProductAsync(string id)
		{
			await _productRepository.DeleteAsync(id);
		}

		public async Task<List<ResultProductDto>> GetAllProductsAsync()
		{
			var products = await _productRepository.GetAllAsync();
			return _mapper.Map<List<ResultProductDto>>(products);
		}

		public async Task<GetByIdProductDto> GetProductByIdAsync(string id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			return _mapper.Map<GetByIdProductDto>(product);
		}

		public async Task<List<ResultProductDto>> GetProductsByPageAsync(int page, int size)
		{
			var products = await _productRepository.GetProductsByPageAsync(page, size);
			return _mapper.Map<List<ResultProductDto>>(products);
		}

		public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
		{
			var product = _mapper.Map<Product>(updateProductDto);
			await _productRepository.UpdateAsync(product);
		}
	}
}
