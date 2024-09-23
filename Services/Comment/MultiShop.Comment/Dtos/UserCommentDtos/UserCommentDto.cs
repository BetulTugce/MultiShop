using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Dtos.UserCommentDtos
{
    public class UserCommentDto
    {
        public int Id { get; set; }
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
        public static implicit operator UserComment(UserCommentDto model)
        {
            return new UserComment
            {
                Id = model.Id,
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

        public static implicit operator UserCommentDto(UserComment model)
        {
            return new UserCommentDto
            {
                Id = model.Id,
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
