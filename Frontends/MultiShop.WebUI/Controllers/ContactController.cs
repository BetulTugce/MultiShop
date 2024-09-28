using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.Contact;
using MultiShop.WebUI.Models.ViewModels.Comment.UserComment;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class ContactController : Controller
    {
        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactCreateVM contactCreateVM)
        {
            var client = _httpClientFactory.CreateClient();
            contactCreateVM.CreatedDate = DateTime.UtcNow;
            // userCommentCreateVM modeli JSON formatına dönüştürülüyor..
            var jsonData = JsonConvert.SerializeObject(contactCreateVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44326/api/Contacts", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                // Form başarılı şekilde gönderildiyse TempData ile başarı mesajı taşınıyor..
                TempData["SuccessMessage"] = "Mesajınız başarıyla iletilmiştir.";
                return RedirectToAction("Index", "Contact");
            }
            // Hata durumunda bir mesaj döndürüyor..
            TempData["ErrorMessage"] = "Mesajınız gönderilirken bir hata oluştu. Lütfen tekrar deneyin.";
            return RedirectToAction("Index", "Contact");
        }
    }
}
