using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // Ürünleri kategorilerine göre listeler..
        public IActionResult Index(string id, int page = 1, int size = 10)
        {
            ViewBag.Id = id;
            ViewBag.Page = page;
            ViewBag.Size = size;
            return View();
        }

        public IActionResult ProductDetail()
        {
            return View();
        }
    }
}
