using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    // Anasayfayı temsil ediyor..
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
