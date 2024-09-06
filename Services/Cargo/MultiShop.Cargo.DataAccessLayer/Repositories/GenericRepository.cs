using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.Repositories
{
	public class GenericRepository<T> : IGenericDal<T> where T : class
	{
		private readonly CargoContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(CargoContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbSet.AnyAsync(expression);
		}

		public IQueryable<T> GetAll()
		{
			return _dbSet.AsQueryable();
		}

		public async Task<T> GetByIdAsync(Guid id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
		{
			var query = _dbSet.AsQueryable();
			if (!tracking)
				query = _dbSet.AsNoTracking();
			return await query.FirstOrDefaultAsync(method);
		}

		public async Task InsertAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			_context.SaveChanges();
		}

		public void Remove(Guid id)
		{
			var entity = _dbSet.Find(id);
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity), "Entity not found.");
			}

			_dbSet.Remove(entity);
			_context.SaveChanges();
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
			_context.SaveChanges();

			//EntityEntry response = _dbSet.Update(entity);
			//return response.State == EntityState.Modified;
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			return _dbSet.Where(expression);
		}
	}
}
