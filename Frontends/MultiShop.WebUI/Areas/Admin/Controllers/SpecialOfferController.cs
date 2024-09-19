using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.SpecialOffer;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/SpecialOffer/")]
    public class SpecialOfferController : Controller
    {
        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public SpecialOfferController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            // Dinamik olarak verileri taşımak için ViewBag kullanılıyor..
            ViewBag.v0 = "Özel Teklifler";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Özel Teklifler Listesi";

            // HTTP istekleri için bir HttpClient oluşturuluyor..
            var client = _httpClientFactory.CreateClient();
            // APIdan kategorileri almak için bir GET isteği gönderiliyor..
            var responseMessage = await client.GetAsync("https://localhost:44326/api/SpecialOffers");
            // Eğer istek başarılıysa (HTTP 200 gibi bir başarı kodu döndüyse) bu blok çalışır.
            if (responseMessage.IsSuccessStatusCode)
            {
                // Gelen yanıtın içeriği string formatında okunuyor..
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // JSON formatındaki veriyi SpecialOfferListVM türünde bir listeye dönüştürülüyor..
                var values = JsonConvert.DeserializeObject<List<SpecialOfferListVM>>(jsonData);
                // Verileri View ile birlikte döndürüyor..
                return View(values);
            }
            // Eğer istek başarısız olursa (örneğin api kapalıysa ya da yanıt 500 dönüyorsa), boş bir View döndürüyor..
            return View();
        }

        [Route("CreateSpecialOffer")]
        [HttpGet]
        public IActionResult CreateSpecialOffer()
        {
            // Dinamik olarak verileri taşımak için ViewBag kullanılıyor..
            ViewBag.v0 = "Özel Teklifler";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Yeni Özel Teklif";
            return View();
        }

        [Route("CreateSpecialOffer")]
        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(SpecialOfferCreateVM specialOfferCreateVM)
        {
            var client = _httpClientFactory.CreateClient();
            // specialOfferCreateVM modeli JSON formatına dönüştürülüyor..
            var jsonData = JsonConvert.SerializeObject(specialOfferCreateVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44326/api/SpecialOffers", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteSpecialOffer/{id}")]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44326/api/SpecialOffers?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateSpecialOffer/{id}")]
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44326/api/SpecialOffers/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<SpecialOfferUpdateVM>(jsonData);
                return View(value);
            }
            return View();
        }

        [Route("UpdateSpecialOffer/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateSpecialOffer(SpecialOfferUpdateVM specialOfferUpdateVM)
        {
            var client = _httpClientFactory.CreateClient();
            // specialOfferUpdateVM modeli JSON formatına dönüştürülüyor..
            var jsonData = JsonConvert.SerializeObject(specialOfferUpdateVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44326/api/SpecialOffers", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
            }
            return View();
        }
    }
}
