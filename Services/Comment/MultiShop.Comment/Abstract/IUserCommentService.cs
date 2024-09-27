using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Abstract
{
    public interface IUserCommentService : IGenericService<UserComment>
    {
        Task<List<UserComment>> GetCommentsByProductIdAsync(string productId, bool isApproved, int? rating);
        Task<List<UserComment>> GetCommentsByProductIdAsync(string productId, bool isApproved, int page, int size);
        Task MarkAsApproved(int id);
    }
}
