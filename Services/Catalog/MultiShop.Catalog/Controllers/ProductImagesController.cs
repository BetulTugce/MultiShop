using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Services.Abstractions;

namespace MultiShop.Catalog.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductImagesController : ControllerBase
	{
		private readonly IProductImageService _productImageService;

		public ProductImagesController(IProductImageService productImageService)
		{
			_productImageService = productImageService;
		}

		[HttpGet]
		public async Task<IActionResult> GetProductImages()
		{
			var values = await _productImageService.GetAllProductImagesAsync();
			return Ok(values);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductImagesById(string id)
		{
			var value = await _productImageService.GetProductImageByIdAsync(id);
			return Ok(value);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
		{
			await _productImageService.CreateProductImageAsync(createProductImageDto);
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProductImage(string id)
		{
			await _productImageService.DeleteProductImageAsync(id);
			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
		{
			await _productImageService.UpdateProductImageAsync(updateProductImageDto);
			return Ok();
		}
	}
}
