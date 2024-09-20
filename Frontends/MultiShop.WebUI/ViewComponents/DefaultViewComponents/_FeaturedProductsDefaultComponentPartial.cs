using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.Product;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeaturedProductsDefaultComponentPartial : ViewComponent
    {
        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public _FeaturedProductsDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // HTTP istekleri için bir HttpClient oluşturuluyor..
            var client = _httpClientFactory.CreateClient();
            // APIdan öne çıkan ürünleri almak için bir GET isteği gönderiliyor..
            var responseMessage = await client.GetAsync("https://localhost:44326/api/Products/GetFeaturedProducts");
            // Eğer istek başarılıysa (HTTP 200 gibi bir başarı kodu döndüyse) bu blok çalışır.
            if (responseMessage.IsSuccessStatusCode)
            {
                // Gelen yanıtın içeriği string formatında okunuyor..
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // JSON formatındaki veriyi ProductListVM türünde bir listeye dönüştürülüyor..
                var values = JsonConvert.DeserializeObject<List<ProductListVM>>(jsonData);
                // Verileri View ile birlikte döndürüyor..
                return View(values);
            }
            // Eğer istek başarısız olursa (örneğin api kapalıysa ya da yanıt 500 dönüyorsa), boş bir View döndürüyor..
            return View();
        }
    }
}
