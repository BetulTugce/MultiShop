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
    }
}
