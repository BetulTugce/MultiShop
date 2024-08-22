namespace MultiShop.Catalog.Dtos.ProductImageDtos
{
	public class GetByIdProductImageDto
	{
		public string Id { get; set; }
		public List<string> Images { get; set; } = new List<string>();

		public string ProductId { get; set; }
	}
}
