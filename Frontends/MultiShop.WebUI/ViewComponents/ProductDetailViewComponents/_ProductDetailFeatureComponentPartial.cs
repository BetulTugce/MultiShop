using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.Product;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailFeatureComponentPartial : ViewComponent
    {
        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailFeatureComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id) 
        {
            // Ürüne ait bilgileri getiriyor..
            var client2 = _httpClientFactory.CreateClient();
            var responseMessage = await client2.GetAsync($"https://localhost:44326/api/Products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ProductVM>(jsonData);
                return View(value);
            }
            return View();
        }
    }
}
