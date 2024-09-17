using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.WebUI.Models.ViewModels.Catalog.Category;
using MultiShop.WebUI.Models.ViewModels.Catalog.Product;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product/")]
    public class ProductController : Controller
    {
        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            // Dinamik olarak verileri taşımak için ViewBag kullanılıyor..
            ViewBag.v0 = "Ürünler";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";

            // HTTP istekleri için bir HttpClient oluşturuluyor..
            var client = _httpClientFactory.CreateClient();
            // APIdan kategorileri almak için bir GET isteği gönderiliyor..
            var responseMessage = await client.GetAsync("https://localhost:44326/api/Products");
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

        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            // Dinamik olarak verileri taşımak için ViewBag kullanılıyor..
            ViewBag.v0 = "Ürünler";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Yeni Ürün";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44326/api/Categories");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryListVM>>(jsonData);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            //List<SelectListItem> categoryList = (from item in categories
            //                                     select new SelectListItem
            //                                     {
            //                                         Text = item.Name, // Dropdownda görünecek kısım categorynin adı..
            //                                         Value = item.Id
            //                                     }).ToList();
            //ViewBag.CategoryList = categoryList;
            return View();
        }

        [Route("CreateProduct")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateVM productCreateVM)
        {
            var client = _httpClientFactory.CreateClient();
            // productCreateVM modeli JSON formatına dönüştürülüyor..
            var jsonData = JsonConvert.SerializeObject(productCreateVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:44326/api/Products", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            else if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //TODO: Login sayfasına yönlendirilecek..
                return View();
            }
            return View();
        }
    }
}
