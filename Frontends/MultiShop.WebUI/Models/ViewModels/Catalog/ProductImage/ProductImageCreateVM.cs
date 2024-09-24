namespace MultiShop.WebUI.Models.ViewModels.Catalog.ProductImage
{
    public class ProductImageCreateVM
    {
        public List<string> Images { get; set; } = new List<string>();

        public string ProductId { get; set; }
    }
}
