using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductsViewComponents
{
    public class _ProductsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() { return View(); }
    }
}
