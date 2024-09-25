using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.ProductDetail;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.ProductViewComponents
{
    public class _ProductDetailUpdateFormComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _ProductDetailUpdateFormComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44326/api/ProductDetails/GetProductDetailByProductId/" + productId);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var productDetailUpdateVM = JsonConvert.DeserializeObject<ProductDetailUpdateVM>(jsonData);
                return View(productDetailUpdateVM);
            }
            return View();
        }
    }
}
