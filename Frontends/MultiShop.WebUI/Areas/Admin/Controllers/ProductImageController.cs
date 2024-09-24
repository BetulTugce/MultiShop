using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.ProductImage;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("UpdateProductImage")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductImage(ProductImageUpdateVM productImageUpdateVM, List<IFormFile> UploadedImages, List<string> DeletedImages)
        {
            var client = _httpClientFactory.CreateClient();

            // Silinecek resimler var mı kontrol ediliyor..
            if (DeletedImages is not null && DeletedImages.Any())
            {
                var deleteResult = await DeleteImagesAsync(client, DeletedImages);

                // Eğer işlem başarılı değilse..
                if (!deleteResult) return BadRequest("Resim silme sırasında bir hata meydana geldi.");

                // Silinen resimler modelden kaldırılıyor..
                foreach (var image in DeletedImages)
                {
                    productImageUpdateVM.Images.Remove(image);
                }
            }

            // Yüklenen dosyalar var mı kontrol ediliyor..
            if (UploadedImages != null && UploadedImages.Any())
            {
                var uploadedImagePaths = await UploadImagesAsync(client, UploadedImages);
                if (uploadedImagePaths is null) return BadRequest("Resim yükleme sırasında bir hata meydana geldi.");

                productImageUpdateVM.Images.AddRange(uploadedImagePaths);
            }
            // Model apiye gönderilerek güncelleme yapılıyor..
            var updateResult = await UpdateProductImageAsync(client, productImageUpdateVM);
            if (!updateResult) return BadRequest("Güncelleme işlemi sırasında bir hata meydana geldi.");

            return RedirectToAction("GetProductsWithCategory", "Product", new { area = "Admin" });
        }

        private async Task<bool> DeleteImagesAsync(HttpClient client, List<string> deletedImages)
        {
            var jsonData = JsonConvert.SerializeObject(deletedImages);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:44326/api/ProductImages/DeleteImages", content);
            return response.IsSuccessStatusCode;
        }

        private async Task<List<string>> UploadImagesAsync(HttpClient client, List<IFormFile> uploadedImages)
        {
            using var formContent = new MultipartFormDataContent();
            foreach (var file in uploadedImages)
            {
                if (file.Length > 0)
                {
                    var streamContent = new StreamContent(file.OpenReadStream());
                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    formContent.Add(streamContent, "files", file.FileName);
                }
            }

            var response = await client.PostAsync("https://localhost:44326/api/ProductImages/UploadFiles", formContent);
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<List<string>>() : null;
        }

        private async Task<bool> UpdateProductImageAsync(HttpClient client, ProductImageUpdateVM productImageUpdateVM)
        {
            var jsonData = JsonConvert.SerializeObject(productImageUpdateVM);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:44326/api/ProductImages", content);
            return response.IsSuccessStatusCode;
        }
    }
}
