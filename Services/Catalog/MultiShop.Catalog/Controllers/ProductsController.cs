using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.Abstractions;
using System.Net;

namespace MultiShop.Catalog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			var values = await _productService.GetAllProductsAsync();
			return Ok(values);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(string id)
		{
			var value = await _productService.GetProductByIdAsync(id);
			return Ok(value);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
		{
			await _productService.CreateProductAsync(createProductDto);
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(string id)
		{
			await _productService.DeleteProductAsync(id);
			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			await _productService.UpdateProductAsync(updateProductDto);
			return Ok();
		}
	}
}
