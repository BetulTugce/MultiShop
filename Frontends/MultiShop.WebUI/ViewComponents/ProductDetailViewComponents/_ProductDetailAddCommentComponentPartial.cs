using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Models.ViewModels.Catalog.ProductDetail;
using MultiShop.WebUI.Models.ViewModels.Comment.UserComment;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailAddCommentComponentPartial : ViewComponent
    {
        // IHttpClientFactory nesnesi için field tanımı.. Api istekleri yapmak için httpclient nesnesi üretmeyi sağlar..
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailAddCommentComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            var model = new UserCommentCreateVM
            {
                ProductId = productId,
                Rating = 0 // Default rating
            };

            return View(model);
            //ViewBag.ProductId = productId;
            //return View();
        }
    }
}
