using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ProductsViewComponents
{
    public class _ProductsPaginationComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke() { return View(); }
    }
}
