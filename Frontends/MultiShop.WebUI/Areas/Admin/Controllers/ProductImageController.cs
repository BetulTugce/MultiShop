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
            // Silinecek resimler var mı kontrol ediliyor..
            if (DeletedImages != null && DeletedImages.Count > 0)
            {
                // API'ye silinmesi gereken resimlerin listesini gönderiyoruz
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(DeletedImages);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44326/api/ProductImages/DeleteImages", content);

                if (!response.IsSuccessStatusCode)
                {
                    // Eğer API silme işlemi başarısız olursa hata döner..
                    return BadRequest("İşlem sırasında bir hata meydana geldi. Lütfen tekrar deneyiniz..");
                }
                else
                {
                    // Silinen resimleri modelden kaldırıyoruz
                    foreach (var image in DeletedImages)
                    {
                        productImageUpdateVM.Images.Remove(image);
                    }
                    var jsonData2 = JsonConvert.SerializeObject(productImageUpdateVM);
                    StringContent content2 = new StringContent(jsonData2, Encoding.UTF8, "application/json");
                    var responseMessage2 = await client.PutAsync("https://localhost:44326/api/ProductImages", content2);
                    if (responseMessage2.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetProductsWithCategory", "Product", new { area = "Admin" });
                    }
                    return View();
                }

                
            }

            // Yüklenen dosyalar APIya gönderilmek üzere hazırlanıyor..
            if (UploadedImages != null && UploadedImages.Count > 0)
            {
                var client = _httpClientFactory.CreateClient();

                // Multipart form-data içeriği oluşturuluyor..
                using var formContent = new MultipartFormDataContent();

                // Yüklenen her dosya formDataya ekleniyor
                foreach (var file in UploadedImages)
                {
                    if (file.Length > 0)
                    {
                        var streamContent = new StreamContent(file.OpenReadStream());
                        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                        formContent.Add(streamContent, "files", file.FileName);
                    }
                }

                // Dosyalar apiya gönderiliyor..
                var response = await client.PostAsync("https://localhost:44326/api/ProductImages/UploadFiles", formContent);
                
                if (response.IsSuccessStatusCode) // Dosyalar apiya başarıyla yüklendiyse..
                {
                    // Apidan dönen
                    var data = await response.Content.ReadFromJsonAsync<List<string>>();
                    if (data is not null) { productImageUpdateVM.Images.AddRange(data); }
                    
                    var jsonData2 = JsonConvert.SerializeObject(productImageUpdateVM);
                    StringContent content2 = new StringContent(jsonData2, Encoding.UTF8, "application/json");
                    var responseMessage2 = await client.PutAsync("https://localhost:44326/api/ProductImages", content2);
                    if (responseMessage2.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetProductsWithCategory", "Product", new { area = "Admin" });
                    }
                    return View();
                }
            }
            return View();
        }
    }
}
