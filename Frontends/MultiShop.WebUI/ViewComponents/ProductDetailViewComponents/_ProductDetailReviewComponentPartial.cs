using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.Product;
using MultiShop.WebUI.Models.ViewModels.Comment.UserComment;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailReviewComponentPartial : ViewComponent
    {
        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailReviewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(string productId, int? rating)
        {
            // HTTP istekleri için bir HttpClient oluşturuluyor..
            var client = _httpClientFactory.CreateClient();
            var url = $"https://localhost:44380/api/Comments/GetCommentsByProductId?productId={productId}&isApproved={true}";
            if (rating.HasValue)
            {
                url += $"&rating={rating.Value}";
            }

            // APIdan yorumları almak için bir GET isteği gönderiliyor..
            var responseMessage = await client.GetAsync(url);
            // Eğer istek başarılıysa (HTTP 200 gibi bir başarı kodu döndüyse) bu blok çalışır.
            if (responseMessage.IsSuccessStatusCode)
            {
                // Gelen yanıtın içeriği string formatında okunuyor..
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // JSON formatındaki veriyi UserCommentListVM türünde bir listeye dönüştürülüyor..
                var comments = JsonConvert.DeserializeObject<List<UserCommentListVM>>(jsonData);
                // Verileri View ile birlikte döndürüyor..
                return View(comments);

            }
            // Eğer istek başarısız olursa (örneğin api kapalıysa ya da yanıt 500 dönüyorsa), boş bir View döndürüyor..
            return View();
        }
    }
}
