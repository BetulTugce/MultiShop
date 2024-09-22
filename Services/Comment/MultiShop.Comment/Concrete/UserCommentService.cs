using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Abstract;
using MultiShop.Comment.Contexts;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Concrete
{
    public class UserCommentService : GenericService<UserComment>, IUserCommentService
    {
        public UserCommentService(CommentContext context) : base(context)
        {
        }

        public async Task<List<UserComment>> GetCommentsByProductIdAsync(string productId, bool isApproved)
        {
            return await _context.UserComments.AsNoTracking().Where(i=>i.ProductId == productId && i.IsApproved == isApproved).ToListAsync();
        }

        public async Task<List<UserComment>> GetCommentsByProductIdAsync(string productId, bool isApproved, int page, int size)
        {
            return await _context.UserComments.AsNoTracking().Where
                (i => i.ProductId == productId && i.IsApproved == isApproved)
                .Skip(page * size)
                .Take(size)
                .ToListAsync();
        }
    }
}
