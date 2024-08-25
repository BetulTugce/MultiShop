using MultiShop.Discount.Dtos.CouponDtos;

namespace MultiShop.Discount.Services.Abstractions
{
	public interface IDiscountService
	{
		Task<List<ResultCouponDto>> GetAllCouponsAsync();
		Task CreateCouponAsync(CreateCouponDto createCouponDto);
		Task UpdateCouponAsync(UpdateCouponDto updateCouponDto);
		Task DeleteCouponAsync(int id);
		Task<GetByIdCouponDto> GetByIdCouponAsync(int id);
	}
}
