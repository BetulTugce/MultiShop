using MultiShop.Basket.Dtos;
using MultiShop.Basket.Services.Abstract;
using MultiShop.Basket.Settings;
using System.Text.Json;

namespace MultiShop.Basket.Services.Concrete
{
	public class BasketService : IBasketService
	{
		private readonly RedisService _redisService;

		public BasketService(RedisService redisService)
		{
			_redisService = redisService;
		}

		// Kullanıcının sepetini Redisten siler..
		public async Task DeleteBasketAsync(string userId)
		{
			var status = await _redisService.GetDb().KeyDeleteAsync(userId);
		}

		// Kullanıcının sepetini Redisten alır..
		public async Task<BasketTotalDto> GetBasketAsync(string userId)
		{
			var existBasket = await _redisService.GetDb().StringGetAsync(userId);
			return JsonSerializer.Deserialize<BasketTotalDto>(existBasket);
		}

		// Kullanıcının sepetini Redise kaydeder/günceller..
		public async Task SaveBasketAsync(BasketTotalDto basketTotalDto)
		{
			await _redisService.GetDb().StringSetAsync(basketTotalDto.UserId, JsonSerializer.Serialize(basketTotalDto));
		}
	}
}
