using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.WebUI.Models.ViewModels.Catalog.Category;
using MultiShop.WebUI.Models.ViewModels.Catalog.MultiProduct;
using MultiShop.WebUI.Models.ViewModels.Catalog.Product;
using MultiShop.WebUI.Models.ViewModels.Catalog.ProductDetail;
using MultiShop.WebUI.Models.ViewModels.Catalog.ProductImage;
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
            // APIdan ürünleri almak için bir GET isteği gönderiliyor..
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
        public async Task<IActionResult> CreateProduct(CreateVM createVM, IFormFile UploadedImage, List<IFormFile> UploadedImages)
        {
            ProductCreateVM prodCreateVM = createVM;
            ProductDetailCreateVM prodDetailCreateVM = createVM;
            var client = _httpClientFactory.CreateClient();
            if (UploadedImage is not null)
            {
                var uploadedImagePath = await UploadImageAsync(client, UploadedImage);
                if (uploadedImagePath is null) return BadRequest("Resim yükleme sırasında bir hata meydana geldi.");

                prodCreateVM.ImageUrl = uploadedImagePath;
                // productCreateVM modeli JSON formatına dönüştürülüyor..
                var jsonData = JsonConvert.SerializeObject(prodCreateVM);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44326/api/Products", content);
                if (response.IsSuccessStatusCode)
                {
                    // Yanıtı okunuyor..
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // JSON verisi ProductVM modeline çeviriliyor..
                    var productResponse = JsonConvert.DeserializeObject<ProductVM>(responseContent);
                    // Yüklenen dosyalar var mı kontrol ediliyor..
                    if (UploadedImages != null && UploadedImages.Any())
                    {
                        var uploadedImagePaths = await UploadImagesAsync(client, UploadedImages);
                        if (uploadedImagePaths is null) return BadRequest("Resim yükleme sırasında bir hata meydana geldi.");
                        ProductImageCreateVM imageCreateVM = new ProductImageCreateVM()
                        {
                            ProductId = productResponse.Id,
                            Images = uploadedImagePaths
                        };
                        // imageCreateVM modeli JSON formatına dönüştürülüyor..
                        var jsonData2 = JsonConvert.SerializeObject(imageCreateVM);
                        StringContent content2 = new StringContent(jsonData2, Encoding.UTF8, "application/json");
                        var response2 = await client.PostAsync("https://localhost:44326/api/ProductImages", content2);

                        prodDetailCreateVM.ProductId = productResponse.Id;

                        // productDetailCreateVM modeli JSON formatına dönüştürülüyor..
                        var jsonData3 = JsonConvert.SerializeObject(prodDetailCreateVM);
                        StringContent content3 = new StringContent(jsonData3, Encoding.UTF8, "application/json");
                        var response3 = await client.PostAsync("https://localhost:44326/api/ProductDetails", content3);

                    }

                    return RedirectToAction("GetProductsWithCategory", "Product", new { area = "Admin" });
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    //TODO: Login sayfasına yönlendirilecek..
                    return View();
                }
            }
            return View();
        }

        private async Task<string> UploadImageAsync(HttpClient client, IFormFile uploadedImage)
        {
            using var formContent = new MultipartFormDataContent();

            if (uploadedImage.Length > 0)
            {
                var streamContent = new StreamContent(uploadedImage.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(uploadedImage.ContentType);
                formContent.Add(streamContent, "file", uploadedImage.FileName);
            }

            var response = await client.PostAsync("https://localhost:44326/api/Products/UploadFile", formContent);

            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }

        private async Task<List<string>> UploadImagesAsync(HttpClient client, List<IFormFile> uploadedImages)
        {
            using var formContent = new MultipartFormDataContent();
            foreach (var file in uploadedImages)
            {
                if (file.Length > 0)
                {
                    var streamContent = new StreamContent(file.OpenReadStream());
                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    formContent.Add(streamContent, "files", file.FileName);
                }
            }

            var response = await client.PostAsync("https://localhost:44326/api/ProductImages/UploadFiles", formContent);
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<List<string>>() : null;
        }

        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44326/api/Products?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                //return RedirectToAction("Index", "Product", new { area = "Admin" });
                return RedirectToAction("GetProductsWithCategory", "Product", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var client = _httpClientFactory.CreateClient();
            // Kategorileri getiriyor..
            var responseMessageForCategories = await client.GetAsync("https://localhost:44326/api/Categories");
            var jsonCategoryData = await responseMessageForCategories.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryListVM>>(jsonCategoryData);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            // Ürüne ait bilgileri getiriyor..
            var client2 = _httpClientFactory.CreateClient();
            var responseMessage = await client2.GetAsync($"https://localhost:44326/api/Products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ProductUpdateVM>(jsonData);
                return View(value);
            }
            return View();
        }

        [Route("UpdateProduct/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpdateVM productUpdateVM, IFormFile UploadedImage)
        {
            var client = _httpClientFactory.CreateClient();
            if (UploadedImage is not null)
            {
                var uploadedImagePath = await UploadImageAsync(client, UploadedImage);
                if (uploadedImagePath is null) return BadRequest("Resim yükleme sırasında bir hata meydana geldi.");

                productUpdateVM.ImageUrl = uploadedImagePath;
            }
            // productUpdateVM modeli JSON formatına dönüştürülüyor..
            var jsonData = JsonConvert.SerializeObject(productUpdateVM);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:44326/api/Products", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                //return RedirectToAction("Index", "Product", new { area = "Admin" });
                return RedirectToAction("GetProductsWithCategory", "Product", new { area = "Admin" });
            }
            return View();
        }

        [Route("GetProductsWithCategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            // Dinamik olarak verileri taşımak için ViewBag kullanılıyor..
            ViewBag.v0 = "Ürünler";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";

            // HTTP istekleri için bir HttpClient oluşturuluyor..
            var client = _httpClientFactory.CreateClient();
            // APIdan kategorileri almak için bir GET isteği gönderiliyor..
            var responseMessage = await client.GetAsync("https://localhost:44326/api/Products/GetProductsWithCategory");
            // Eğer istek başarılıysa (HTTP 200 gibi bir başarı kodu döndüyse) bu blok çalışır.
            if (responseMessage.IsSuccessStatusCode)
            {
                // Gelen yanıtın içeriği string formatında okunuyor..
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // JSON formatındaki veriyi ProductWithCategoryVM türünde bir listeye dönüştürülüyor..
                var values = JsonConvert.DeserializeObject<List<ProductWithCategoryVM>>(jsonData);
                // Verileri View ile birlikte döndürüyor..
                return View(values);
            }
            // Eğer istek başarısız olursa (örneğin api kapalıysa ya da yanıt 500 dönüyorsa), boş bir View döndürüyor..
            return View();
        }
    }
}
