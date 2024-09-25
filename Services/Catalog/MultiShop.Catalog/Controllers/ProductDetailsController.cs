using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Services.Abstractions;
using MultiShop.Catalog.Services.Concrete;

namespace MultiShop.Catalog.Controllers
{
	[Authorize(Policy = "CatalogFullPermission")]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductDetailsController : ControllerBase
	{
		private readonly IProductDetailService _productDetailService;

		public ProductDetailsController(IProductDetailService productDetailService)
		{
			_productDetailService = productDetailService;
		}

		[HttpGet]
		public async Task<IActionResult> GetProductDetails()
		{
			var values = await _productDetailService.GetAllProductDetailsAsync();
			return Ok(values);
		}

		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductDetailById(string id)
		{
			var value = await _productDetailService.GetProductDetailByIdAsync(id);
			return Ok(value);
		}

        [AllowAnonymous]
        [HttpGet("GetProductDetailByProductId/{productId}")]
        public async Task<IActionResult> GetProductDetailByProductId([FromRoute] string productId)
        {
            var value = await _productDetailService.GetProductDetailByProductIdAsync(productId);
            return Ok(value);
        }

        [HttpPost]
		public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
		{
			await _productDetailService.CreateProductDetailAsync(createProductDetailDto);
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProductDetail(string id)
		{
			await _productDetailService.DeleteProductDetailAsync(id);
			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
		{
			await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
			return Ok();
		}
	}
}
