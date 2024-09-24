using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.ProductImage;
using Newtonsoft.Json;

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
                return View(value);
            }
            return View();
        }
    }
}
