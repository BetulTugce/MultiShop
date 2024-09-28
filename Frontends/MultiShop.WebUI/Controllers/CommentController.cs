using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Comment.UserComment;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class CommentController : Controller
    {

        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(UserCommentCreateVM userCommentCreateVM)
        {
            var client = _httpClientFactory.CreateClient();
            userCommentCreateVM.CreatedDate = DateTime.UtcNow;
            // userCommentCreateVM modeli JSON formatına dönüştürülüyor..
            var jsonData = JsonConvert.SerializeObject(userCommentCreateVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44380/api/Comments", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                // Yorum başarılı şekilde gönderildiyse TempData ile başarı mesajı taşınıyor..
                TempData["SuccessMessage"] = "Yorumunuz başarıyla gönderildi. Onaylandıktan sonra görünecektir.";
                return RedirectToAction("ProductDetail", "Product", new { id = userCommentCreateVM.ProductId });
            }
            // Hata durumunda bir mesaj döndürüyor..
            TempData["ErrorMessage"] = "Yorum gönderilirken bir hata oluştu. Lütfen tekrar deneyin.";
            return RedirectToAction("ProductDetail", "Product", new { id = userCommentCreateVM.ProductId });
        }
    }
}
