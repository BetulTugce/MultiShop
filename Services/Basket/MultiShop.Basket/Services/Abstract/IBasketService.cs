using MultiShop.Basket.Dtos;

namespace MultiShop.Basket.Services.Abstract
{
	public interface IBasketService
	{
		// Kullanıcının sepetini getirir..
		Task<BasketTotalDto> GetBasketAsync(string userId);

		// Kullanıcının sepetini kaydeder..
		Task SaveBasketAsync(BasketTotalDto basketTotalDto, string userId);

		// Kullanıcının sepetini siler..
		Task DeleteBasketAsync(string userId);
	}
}
