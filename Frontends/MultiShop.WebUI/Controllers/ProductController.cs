using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.MultiProduct;

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

        //public IActionResult ProductDetail(string id)
        //{
        //    ViewBag.Id = id;
        //    return View();
        //}
        
        public IActionResult ProductDetail(string id)
        {
            var model = new ProductDetailIdVM
            {
                ProductId = id
            };
            return View(model);

        }
    }
}
