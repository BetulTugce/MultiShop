using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.FeatureSlider;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/FeatureSlider/")]
    public class FeatureSliderController : Controller
    {
        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureSliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            // Dinamik olarak verileri taşımak için ViewBag kullanılıyor..
            ViewBag.v0 = "Öne Çıkanlar Sliderı";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkanlar Sliderı";
            ViewBag.v3 = "Öne Çıkanlar Slider Listesi";

            // HTTP istekleri için bir HttpClient oluşturuluyor..
            var client = _httpClientFactory.CreateClient();
            // APIdan kategorileri almak için bir GET isteği gönderiliyor..
            var responseMessage = await client.GetAsync("https://localhost:44326/api/FeatureSliders");
            // Eğer istek başarılıysa (HTTP 200 gibi bir başarı kodu döndüyse) bu blok çalışır.
            if (responseMessage.IsSuccessStatusCode)
            {
                // Gelen yanıtın içeriği string formatında okunuyor..
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // JSON formatındaki veriyi FeatureSliderListVM türünde bir listeye dönüştürülüyor..
                var values = JsonConvert.DeserializeObject<List<FeatureSliderListVM>>(jsonData);
                // Verileri View ile birlikte döndürüyor..
                return View(values);
            }
            // Eğer istek başarısız olursa (örneğin api kapalıysa ya da yanıt 500 dönüyorsa), boş bir View döndürüyor..
            return View();
        }

        [Route("CreateFeatureSlider")]
        [HttpGet]
        public IActionResult CreateFeatureSlider()
        {
            // Dinamik olarak verileri taşımak için ViewBag kullanılıyor..
            ViewBag.v0 = "Öne Çıkanlar Sliderı";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkanlar Sliderı";
            ViewBag.v3 = "Yeni Öne Çıkan Sliderı";
            return View();
        }

        [Route("CreateFeatureSlider")]
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(FeatureSliderCreateVM featureSliderCreateVM)
        {
            var client = _httpClientFactory.CreateClient();
            // FeatureSliderCreateVM modeli JSON formatına dönüştürülüyor..
            var jsonData = JsonConvert.SerializeObject(featureSliderCreateVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44326/api/FeatureSliders", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteFeatureSlider/{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44326/api/FeatureSliders?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateFeatureSlider/{id}")]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44326/api/FeatureSliders/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<FeatureSliderUpdateVM>(jsonData);
                return View(value);
            }
            return View();
        }

        [Route("UpdateFeatureSlider/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(FeatureSliderUpdateVM featureSliderUpdateVM)
        {
            var client = _httpClientFactory.CreateClient();
            // FeatureSliderUpdateVM modeli JSON formatına dönüştürülüyor..
            var jsonData = JsonConvert.SerializeObject(featureSliderUpdateVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44326/api/FeatureSliders", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }
            return View();
        }
    }
}
