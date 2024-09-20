using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.About;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About/")]
    public class AboutController : Controller
    {
        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            // Dinamik olarak verileri taşımak için ViewBag kullanılıyor..
            ViewBag.v0 = "Hakkımızda";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Hakkımızda";
            ViewBag.v3 = "Hakkımızda Listesi";

            // HTTP istekleri için bir HttpClient oluşturuluyor..
            var client = _httpClientFactory.CreateClient();
            // APIdan hakkımda yazılarını almak için bir GET isteği gönderiliyor..
            var responseMessage = await client.GetAsync("https://localhost:44326/api/Abouts");
            // Eğer istek başarılıysa (HTTP 200 gibi bir başarı kodu döndüyse) bu blok çalışır.
            if (responseMessage.IsSuccessStatusCode)
            {
                // Gelen yanıtın içeriği string formatında okunuyor..
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // JSON formatındaki veriyi AboutListVM türünde bir listeye dönüştürülüyor..
                var values = JsonConvert.DeserializeObject<List<AboutListVM>>(jsonData);
                // Verileri View ile birlikte döndürüyor..
                return View(values);
            }
            // Eğer istek başarısız olursa (örneğin api kapalıysa ya da yanıt 500 dönüyorsa), boş bir View döndürüyor..
            return View();
        }

        [Route("CreateAbout")]
        [HttpGet]
        public IActionResult CreateAbout()
        {
            // Dinamik olarak verileri taşımak için ViewBag kullanılıyor..
            ViewBag.v0 = "Hakkımızda";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Hakkımızda";
            ViewBag.v3 = "Hakkımızda Listesi";
            return View();
        }

        [Route("CreateAbout")]
        [HttpPost]
        public async Task<IActionResult> CreateAbout(AboutCreateVM aboutCreateVM)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44326/api/Abouts/DeleteAllAbouts");
            if (responseMessage.IsSuccessStatusCode)
            {
                // AboutCreateVM modeli JSON formatına dönüştürülüyor..
                var jsonData = JsonConvert.SerializeObject(aboutCreateVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage2 = await client.PostAsync("https://localhost:44326/api/Abouts", content);
                if (responseMessage2.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "About", new { area = "Admin" });
                }
                
            }
            return View();
        }

        [Route("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44326/api/Abouts?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteAllAbouts")]
        public async Task<IActionResult> DeleteAllAbouts()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44326/api/Abouts/DeleteAllAbouts");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44326/api/Abouts/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<AboutUpdateVM>(jsonData);
                return View(value);
            }
            return View();
        }

        [Route("UpdateAbout/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(AboutUpdateVM aboutUpdateVM)
        {
            var client = _httpClientFactory.CreateClient();
            // AboutUpdateVM modeli JSON formatına dönüştürülüyor..
            var jsonData = JsonConvert.SerializeObject(aboutUpdateVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44326/api/Abouts", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About", new { area = "Admin" });
            }
            return View();
        }
    }
}
