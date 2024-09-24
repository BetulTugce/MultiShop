namespace MultiShop.WebUI.Models.ViewModels.Catalog.ProductImage
{
    public class ProductImageUpdateVM
    {
        public string Id { get; set; }
        public List<string> Images { get; set; } = new List<string>();

        public string ProductId { get; set; }

        // Dosya yükleme işlemi için
        public List<IFormFile> UploadedImages { get; set; }  // Birden fazla fotoğraf yükleme
    }
}
