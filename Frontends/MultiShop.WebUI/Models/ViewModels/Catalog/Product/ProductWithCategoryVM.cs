using MultiShop.WebUI.Models.ViewModels.Catalog.Category;

namespace MultiShop.WebUI.Models.ViewModels.Catalog.Product
{
    public class ProductWithCategoryVM
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public string CategoryId { get; set; }
        public CategoryVM Category { get; set; }

        // Kuponsuz indirim oranı
        public int? DiscountRate { get; set; }
        public bool IsFeatured { get; set; }
    }
}
