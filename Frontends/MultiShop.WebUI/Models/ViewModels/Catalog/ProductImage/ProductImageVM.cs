namespace MultiShop.WebUI.Models.ViewModels.Catalog.ProductImage
{
    public class ProductImageVM
    {
        public string Id { get; set; }
        public List<string> Images { get; set; } = new List<string>();

        public string ProductId { get; set; }
    }
}
