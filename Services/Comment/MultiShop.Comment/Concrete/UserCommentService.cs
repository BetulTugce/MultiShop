using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Abstract;
using MultiShop.Comment.Contexts;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Concrete
{
    public class UserCommentService : GenericService<UserComment>, IUserCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserCommentService(CommentContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserComment>> GetCommentsByProductIdAsync(string productId, bool isApproved)
        {
            return await _context.UserComments.AsNoTracking().Where(i => i.ProductId == productId && i.IsApproved == isApproved).ToListAsync();
        }

        public async Task<List<UserComment>> GetCommentsByProductIdAsync(string productId, bool isApproved, int page, int size)
        {
            return await _context.UserComments.AsNoTracking().Where
                (i => i.ProductId == productId && i.IsApproved == isApproved)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task MarkAsApproved(int id)
        {
            //var comment = await _context.UserComments.Where(i => i.Id == id).FirstOrDefaultAsync();
            var comment = _context.UserComments.Find(id);
            if (comment is not null)
            {
                comment.IsApproved = comment.IsApproved ? false : true;
                _context.UserComments.Update(comment);
                await _unitOfWork.CommitAsync();
            }

        }
    }
}
