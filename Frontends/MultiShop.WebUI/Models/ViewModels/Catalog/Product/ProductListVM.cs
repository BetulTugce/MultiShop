namespace MultiShop.WebUI.Models.ViewModels.Catalog.Product
{
    public class ProductListVM
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public string CategoryId { get; set; }

        // Kuponsuz indirim oranı
        public int? DiscountRate { get; set; }
    }
}
