namespace MultiShop.WebUI.Models.ViewModels.Catalog.FeatureSlider
{
    public class FeatureSliderUpdateVM
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; } // Aktif-Pasif olma durumu
    }
}
