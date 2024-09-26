using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.Product;
using MultiShop.WebUI.Models.ViewModels.Comment.UserComment;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Comment/")]
    public class CommentController : Controller
    {
        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            // Dinamik olarak verileri taşımak için ViewBag kullanılıyor..
            ViewBag.v0 = "Yorumlar";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Yorumlar";
            ViewBag.v3 = "Yorum Listesi";

            // HTTP istekleri için bir HttpClient oluşturuluyor..
            var client = _httpClientFactory.CreateClient();
            // APIdan yorumları almak için bir GET isteği gönderiliyor..
            var responseMessage = await client.GetAsync("https://localhost:44380/api/Comments");
            // Eğer istek başarılıysa (HTTP 200 gibi bir başarı kodu döndüyse) bu blok çalışır.
            if (responseMessage.IsSuccessStatusCode)
            {
                // Gelen yanıtın içeriği string formatında okunuyor..
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // JSON formatındaki veriyi UserCommentListVM türünde bir listeye dönüştürülüyor..
                var comments = JsonConvert.DeserializeObject<List<UserCommentListVM>>(jsonData);

                foreach (var comment in comments)
                {
                    // Ürüne dair bilgileri almak için productId kullanılarak istek yapılıyor.
                    var productResponse = await client.GetAsync($"https://localhost:44326/api/Products/{comment.ProductId}");

                    if (productResponse.IsSuccessStatusCode)
                    {
                        // Ürünün adı gibi bilgileri alıp yorum nesnesine ekleniyor.
                        var productData = await productResponse.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<ProductVM>(productData);

                        comment.Product = product;
                    }

                    
                }
                // Verileri View ile birlikte döndürüyor..
                return View(comments);

            }
            // Eğer istek başarısız olursa (örneğin api kapalıysa ya da yanıt 500 dönüyorsa), boş bir View döndürüyor..
            return View();
        }

        [Route("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44380/api/Comments?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateComment/{id}")]
        public async Task<IActionResult> UpdateComment(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44380/api/Comments/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UserCommentUpdateVM>(jsonData);
                return View(value);
            }
            return View();
        }

        [Route("UpdateComment/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateComment(UserCommentUpdateVM userCommentUpdateVM)
        {
            var client = _httpClientFactory.CreateClient();
            // userCommentUpdateVM modeli JSON formatına dönüştürülüyor..
            var jsonData = JsonConvert.SerializeObject(userCommentUpdateVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44380/api/Comments", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment", new { area = "Admin" });
            }
            return View();
        }
    }
}
