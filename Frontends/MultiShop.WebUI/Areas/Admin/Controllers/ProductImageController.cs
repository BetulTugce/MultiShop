using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Models.ViewModels.Catalog.ProductImage;
using MultiShop.WebUI.Services;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly FileService _fileService;

        public ProductImageController(IHttpClientFactory httpClientFactory, FileService fileService)
        {
            _httpClientFactory = httpClientFactory;
            _fileService = fileService;
        }

        //[Route("UpdateProductImage/{id}")]
        //public async Task<IActionResult> UpdateProductImage(string id)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    var responseMessage = await client.GetAsync("https://localhost:44326/api/ProductImages/GetProductImageByProductId/" + id);
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        //        var values = JsonConvert.DeserializeObject<ProductImageUpdateVM>(jsonData);
        //        return View(values);
        //    }
        //    return View();
        //}

        [Route("UpdateProductImage")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductImage(ProductImageUpdateVM productImageUpdateVM)
        {
            if (productImageUpdateVM.UploadedImages != null && productImageUpdateVM.UploadedImages.Count > 0)
            {
                foreach (var file in productImageUpdateVM.UploadedImages)
                {
                    // Dosya boyutu kontrol ediliyor..
                    if (file.Length > 10 * 1024 * 1024) // 10MB sınırı
                    {
                        // Dosya boyutu 10MB'tan büyük olamaz uyarısı verilecek..
                        //return BadRequest("Dosya boyutu 10MB'tan büyük olamaz.");
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
                    // Daha sonra kaydettiğiniz dosyanın URL'sini veritabanına ekleyebilirsiniz.
                    productImageUpdateVM.Images.Add($"/{ImageDirectory.ProductImages}/{randomFileName}");
                }
            }

            var client = _httpClientFactory.CreateClient();
            ProductImageVM productImageVM = new ProductImageVM()
            {
                Id = productImageUpdateVM.Id,
                ProductId = productImageUpdateVM.ProductId,
                Images = productImageUpdateVM.Images,
            };
            var jsonData = JsonConvert.SerializeObject(productImageVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44326/api/ProductImages", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("GetProductsWithCategory", "Product", new { area = "Admin" });
            }
            return View();
        }
    }
}
