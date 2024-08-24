namespace MultiShop.Discount.Dtos.CouponDtos
{
	public class ResultCouponDto
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public int Rate { get; set; }
		public bool IsActive { get; set; }
		public DateTime ValidDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
	}
}
