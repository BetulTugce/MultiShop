using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Basket.Dtos;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services.Abstract;

namespace MultiShop.Basket.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketsController : ControllerBase
	{
		private readonly IBasketService _basketService;
		private readonly ILoginService _loginService;

		public BasketsController(IBasketService basketService, ILoginService loginService)
		{
			_basketService = basketService;
			_loginService = loginService;
		}

		// Kullanıcının sepetini getirir..
		[HttpGet]
		public async Task<IActionResult> GetMyBasket() 
		{
			//var user = User.Claims;
			var values = await _basketService.GetBasketAsync(_loginService.GetUserId);
			return Ok(values);
		}

		// Kullanıcının sepetini kaydeder..
		[HttpPost]
		public async Task<IActionResult> SaveMyBasket(BasketTotalDto dto)
		{
			//dto.UserId = _loginService.GetUserId;
			await _basketService.SaveBasketAsync(dto, _loginService.GetUserId);
			return Ok();
		}

		// Kullanıcının sepetini siler..
		[HttpDelete]
		public async Task<IActionResult> DeleteBasket()
		{
			await _basketService.DeleteBasketAsync(_loginService.GetUserId);
			return NoContent();
		}
	}
}
