using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.Abstractions;
using System.Net;

namespace MultiShop.Catalog.Controllers
{
	[Authorize(Policy = "CatalogFullPermission")]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> GetProducts()
		{
			var values = await _productService.GetAllProductsAsync();
			return Ok(values);
		}

		[AllowAnonymous]
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

		[AllowAnonymous]
		// Method adı dinamik olarak routea ekleniyor.. /api/ControllerName/GetProductsByPage
		[HttpPost("[action]")]
		public async Task<IActionResult> GetProductsByPage(int page, int size)
		{
			var products = await _productService.GetProductsByPageAsync(page, size);
			if (!products.Any())
			{
				return NotFound();
			}
			return Ok(products);
		}

		[AllowAnonymous]
		//[HttpGet("ByCategory")]
		[HttpGet("[action]")]
		public async Task<IActionResult> GetProductsByCategoryAndPage([FromQuery] string categoryId, [FromQuery] int page = 1, [FromQuery] int size = 10)
		{
			var products = await _productService.GetProductsByCategoryAndPageAsync(categoryId, page, size);

			if (!products.Any())
			{
				return NotFound("No products found for the given category.");
			}
			return Ok(products);
		}
	}
}
