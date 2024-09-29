using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
