using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.Product;
using MultiShop.WebUI.Models.ViewModels.Catalog.ProductImage;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.ProductViewComponents
{
    public class _ProductImageUpdateFormComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _ProductImageUpdateFormComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44326/api/ProductImages/GetProductImageByProductId/" + productId);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var productImageVM = JsonConvert.DeserializeObject<ProductImageVM>(jsonData);
                ProductImageUpdateVM value = new ProductImageUpdateVM()
                {
                    Images = productImageVM.Images,
                    ProductId = productImageVM.ProductId,
                    Id = productImageVM.Id
                };
                List<string> imgs = new List<string>();
                if (value.Images is not null && value.Images.Any())
                {
                    var jsonDataImages = JsonConvert.SerializeObject(value.Images);
                    var imagesContent = new StringContent(jsonDataImages, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44326/api/ProductImages/GetProductImagesBase64", imagesContent);
                    if (response.IsSuccessStatusCode) 
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        // JSON verisi ProductVM modeline çeviriliyor..
                        imgs = JsonConvert.DeserializeObject<List<string>>(responseContent);
                    }
                }

                //return View(value);
                return View(new { ProductImageUpdateVM = value, Base64Images = imgs }); // İki nesneyi bir arada gönderiyoruz
            }
            return View();
        }
    }
}
