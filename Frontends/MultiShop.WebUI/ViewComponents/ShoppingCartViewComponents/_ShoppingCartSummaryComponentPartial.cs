using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ShoppingCartViewComponents
{
    public class _ShoppingCartSummaryComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
