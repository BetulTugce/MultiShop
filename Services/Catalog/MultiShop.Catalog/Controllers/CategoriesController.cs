using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.Abstractions;
using SharpCompress.Common;

namespace MultiShop.Catalog.Controllers
{
	[Authorize(Policy = "CatalogFullPermission")]
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		//[Authorize(Policy = "CatalogReadOrFullPermission")]
		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{
			var values = await _categoryService.GetAllCategoriesAsync();
			return Ok(values);
		}

		[AllowAnonymous]
		//[Authorize(Policy = "CatalogReadOrFullPermission")]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById(string id)
		{
			var value = await _categoryService.GetCategoryByIdAsync(id);
			return Ok(value);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
		{
			await _categoryService.CreateCategoryAsync(createCategoryDto);
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteCategory(string id)
		{
			await _categoryService.DeleteCategoryAsync(id);
			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			await _categoryService.UpdateCategoryAsync(updateCategoryDto);
			return Ok();
		}
	}
}
