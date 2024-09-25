using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Enums;
using MultiShop.Catalog.Services;
using MultiShop.Catalog.Services.Abstractions;
using MultiShop.Catalog.Services.Concrete;

namespace MultiShop.Catalog.Controllers
{
	[Authorize(Policy = "CatalogFullPermission")]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductImagesController : ControllerBase
	{
		private readonly IProductImageService _productImageService;
        private readonly FileService _fileService;

        public ProductImagesController(IProductImageService productImageService, FileService fileService)
		{
			_productImageService = productImageService;
			_fileService = fileService;
		}

		[HttpGet]
		public async Task<IActionResult> GetProductImages()
		{
			var values = await _productImageService.GetAllProductImagesAsync();
			return Ok(values);
		}

		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductImagesById(string id)
		{
			var value = await _productImageService.GetProductImageByIdAsync(id);
			return Ok(value);
		}

        [AllowAnonymous]
        [HttpGet("GetProductImageByProductId/{productId}")]
        public async Task<IActionResult> GetProductImageByProductId([FromRoute]string productId)
        {
            var value = await _productImageService.GetProductImageByProductIdAsync(productId);
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

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFiles(IFormFileCollection files)
        {
            // Eğer hiç dosya yoksa 400 döner..
            if (files == null || files.Count == 0)
            {
                return BadRequest("Yüklenecek dosya bulunamadı.");
            }

            var uploadedFilePaths = new List<string>();

            foreach (var file in files)
            {
                // Dosya boyutu kontrolü.. Dosyaların hiçbiri 10MB'tan büyük olmamalı!
                if (file.Length > 10 * 1024 * 1024) // 10MB sınırı
                {
                    return BadRequest("Dosya boyutu 10MB'tan büyük olamaz: " + file.FileName);
                }

                // Dosya adı rastgele oluşturuluyor..
                string randomFileName = _fileService.GenerateRandomFileName(file.FileName);

                // Dosya yolu alınıyor..
                string filePath = _fileService.GetFilePath(randomFileName, ImageDirectory.ProductImages.ToString());

                // Dosya kaydediliyor..
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Kaydedilen dosyanın yolu listeye ekleniyor..
                uploadedFilePaths.Add($"/{ImageDirectory.ProductImages}/{randomFileName}");
                //uploadedFilePaths.Add($"{randomFileName}");
            }

            // Dosya yollarını geri döndürüyor..
            return Ok(uploadedFilePaths);
        }

        [HttpPost("[action]")]
        public IActionResult DeleteImages([FromBody] List<string> imagePaths)
        {
            // Eğer liste boşsa 400 döner..
            if (imagePaths == null || imagePaths.Count == 0)
            {
                return BadRequest("Silinecek resim bulunamadı.");
            }

            foreach (var imagePath in imagePaths)
            {
                // Resmin tam dosya yolunu alıyoruz
                _fileService.DeleteImage(imagePath, ImageDirectory.ProductImages.ToString());
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetProductImagesBase64(List<string> imagePaths)
        {
            if (imagePaths == null)
            {
                return NotFound();
            }

            var imagesBase64 = new List<string>();
            foreach (var imageName in imagePaths)
            {
                string imgName = Path.GetFileName(imageName);
                byte[] imageBytes = await _fileService.GetImageAsync(imgName, ImageDirectory.ProductImages.ToString());
                imagesBase64.Add(Convert.ToBase64String(imageBytes));
            }
            return Ok(imagesBase64); // Resimler base64 formatında JSON olarak döndürülüyor..
        }
    }
}
