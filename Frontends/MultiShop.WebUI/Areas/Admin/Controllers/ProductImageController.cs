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
            // Yüklenen dosyalar APIya gönderilmek üzere hazırlanıyor..
            if (productImageUpdateVM.UploadedImages != null && productImageUpdateVM.UploadedImages.Count > 0)
            {
                var client = _httpClientFactory.CreateClient();

                // Multipart form-data içeriği oluşturuluyor..
                using var formContent = new MultipartFormDataContent();

                // Yüklenen her dosya formDataya ekleniyor
                foreach (var file in productImageUpdateVM.UploadedImages)
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
                    ProductImageVM productImageVM2 = new ProductImageVM()
                    {
                        Id = productImageUpdateVM.Id,
                        ProductId = productImageUpdateVM.ProductId,
                        Images = productImageUpdateVM.Images,
                    };
                    if (data is not null) { productImageVM2.Images.AddRange(data); }
                    
                    var jsonData2 = JsonConvert.SerializeObject(productImageVM2);
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
