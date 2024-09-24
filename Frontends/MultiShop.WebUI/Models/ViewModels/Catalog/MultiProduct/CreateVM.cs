using MultiShop.WebUI.Models.ViewModels.Catalog.Product;
using MultiShop.WebUI.Models.ViewModels.Catalog.ProductDetail;

namespace MultiShop.WebUI.Models.ViewModels.Catalog.MultiProduct
{
    public class CreateVM
    {
        public ProductCreateVM ProductCreateVM { get; set; }
        public ProductDetailCreateVM ProductDetailCreateVM { get; set; }

        #region Implicit/Bilinçsiz Operator Overload ile Dönüştürme
        public static implicit operator ProductCreateVM(CreateVM model)
        {
            return new ProductCreateVM
            {
                CategoryId = model.ProductCreateVM.CategoryId,
                Description = model.ProductCreateVM.Description,
                DiscountRate = model.ProductCreateVM.DiscountRate,
                ImageUrl = model.ProductCreateVM.ImageUrl,
                IsFeatured = model.ProductCreateVM.IsFeatured,
                Name = model.ProductCreateVM.Name,
                Price = model.ProductCreateVM.Price,
            };
        }

        public static implicit operator ProductDetailCreateVM(CreateVM model)
        {
            return new ProductDetailCreateVM
            {
                Description = model.ProductDetailCreateVM.Description,
                Info = model.ProductDetailCreateVM.Info,
                ProductId = model.ProductDetailCreateVM.ProductId,
            };
        }
        #endregion
    }
}
