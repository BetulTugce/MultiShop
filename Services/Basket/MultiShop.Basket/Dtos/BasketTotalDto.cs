namespace MultiShop.Basket.Dtos
{
	public class BasketTotalDto
	{
		public string UserId { get; set; }
		public string DiscountCouponCode { get; set; }
		public int DiscountRate { get; set; } = 0;
		public List<BasketItemDto> BasketItems { get; set; }
		public decimal TotalPrice { get => BasketItems.Sum(x => x.Price * x.Quantity); }
	}
}
