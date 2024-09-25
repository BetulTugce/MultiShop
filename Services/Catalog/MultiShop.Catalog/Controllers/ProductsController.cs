using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Enums;
using MultiShop.Catalog.Services;
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
        private readonly FileService _fileService;

        public ProductsController(IProductService productService, FileService fileService)
		{
			_productService = productService;
			_fileService = fileService;
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

        //[HttpPost]
        //public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        //{
        //	await _productService.CreateProductAsync(createProductDto);
        //	return StatusCode(StatusCodes.Status201Created);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var prod = await _productService.CreateProductAsync(createProductDto);
            return StatusCode(201, prod);
        }

        [HttpDelete]
		public async Task<IActionResult> DeleteProduct(string id)
		{
			await _productService.DeleteProductAsync(id);
			return NoContent();
		}

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProductByProductId(string id)
        {
            var response = await _productService.DeleteProductByProductIdAsync(id);
            if (response)
            {
                return NoContent();
            }
            return BadRequest();
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

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var products = await _productService.GetProductsWithCategoryAsync();

            if (!products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFeaturedProducts()
        {
            var products = await _productService.GetFeaturedProductsAsync();

            if (!products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("GetProductCoverImage/{id}")]
        public async Task<IActionResult> GetProductCoverImage([FromRoute] string id)
        {
            var response = await _productService.GetProductCoverImageByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            string imgName = Path.GetFileName(response);
            byte[] imageBytes = await _fileService.GetImageAsync(imgName, ImageDirectory.ProductCoverImages.ToString());

            return File(imageBytes, "image/jpeg"); // Resmi JPEG olarak döndürüyor..

            //return Ok(Convert.ToBase64String(imageBytes));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            // Dosya boyutu kontrol ediliyor..
            if (file.Length > 10 * 1024 * 1024) // 10MB sınırı
            {
                return BadRequest("Dosya boyutu 10MB'tan büyük olamaz.");
            }
            // Dosya adı rastgele oluşturuluyor..
            string randomFileName = _fileService.GenerateRandomFileName(file.FileName);

            // Dosya yolu alınıyor..
            string filePath = _fileService.GetFilePath(randomFileName, ImageDirectory.ProductCoverImages.ToString());

            // Dosya kaydediliyor..
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //return Ok(new { FileName = randomFileName, FilePath = filePath });
            return Ok($"/{ImageDirectory.ProductCoverImages}/{randomFileName}");
        }

        [HttpPost("[action]")]
        public IActionResult DeleteImage([FromBody] string imagePath)
        {
            if (imagePath == null)
            {
                return BadRequest("Resim bulunamadı.");
            }

            _fileService.DeleteImage(imagePath, ImageDirectory.ProductCoverImages.ToString());

            return Ok();
        }
    }
}
