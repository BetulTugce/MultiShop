using MultiShop.Comment.Abstract;
using MultiShop.Comment.Contexts;

namespace MultiShop.Comment.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CommentContext _context;

        public UnitOfWork(CommentContext context)
        {
            _context = context;
        }

        // Senkron olarak değişiklikleri kaydetmeyi sağlayan metot.
        public void Commit()
        {
            // SaveChanges metodu, veritabanındaki tüm değişiklikleri kalıcı hale getirir.
            _context.SaveChanges();
        }

        // Asenkron olarak değişiklikleri kaydetmeyi sağlayan metot.
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
