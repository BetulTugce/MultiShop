using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos.CouponDtos;
using MultiShop.Discount.Services.Abstractions;

namespace MultiShop.Discount.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DiscountsController : ControllerBase
	{
		private readonly IDiscountService _discountService;

		public DiscountsController(IDiscountService discountService)
		{
			_discountService = discountService;
		}

		[HttpGet]
		public async Task<IActionResult> GetDiscountCoupons()
		{
			var values = await _discountService.GetAllCouponsAsync();
			return Ok(values);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDiscountCouponById(int id)
		{
			var value = await _discountService.GetByIdCouponAsync(id);
			return Ok(value);
		}

		[HttpPost]
		public async Task<IActionResult> CreateDiscountCoupon(CreateCouponDto createCouponDto)
		{
			await _discountService.CreateCouponAsync(createCouponDto);
			return StatusCode(StatusCodes.Status201Created);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteDiscountCoupon(int id)
		{
			await _discountService.DeleteCouponAsync(id);
			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateDiscountCoupon(UpdateCouponDto updateCouponDto)
		{
			await _discountService.UpdateCouponAsync(updateCouponDto);
			return Ok();
		}
	}
}
