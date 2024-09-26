using MultiShop.WebUI.Models.ViewModels.Catalog.Product;

namespace MultiShop.WebUI.Models.ViewModels.Comment.UserComment
{
    public class UserCommentListVM
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; } // Onaylanan yorumlar uida gösterilecek..

        public string ProductId { get; set; }
        public ProductVM Product { get; set; }
    }
}
