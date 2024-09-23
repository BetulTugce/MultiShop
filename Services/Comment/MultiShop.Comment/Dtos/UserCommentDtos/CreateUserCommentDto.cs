using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Dtos.UserCommentDtos
{
    public class CreateUserCommentDto
    {
        public string Content { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsApproved { get; set; } // Onaylanan yorumlar uida gösterilecek..

        public string ProductId { get; set; }

        #region Implicit/Bilinçsiz Operator Overload ile Dönüştürme
        public static implicit operator UserComment(CreateUserCommentDto model)
        {
            return new UserComment
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Content = model.Content,
                Email = model.Email,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                IsApproved = model.IsApproved,
                ProductId = model.ProductId,
                Rating = model.Rating
            };
        }
        #endregion
    }
}
