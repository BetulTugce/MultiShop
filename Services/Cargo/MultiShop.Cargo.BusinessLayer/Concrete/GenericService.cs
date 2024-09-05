using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
	public class GenericService<T> : IGenericService<T> where T : class
	{
		private readonly IGenericDal<T> _repository;

		public GenericService(IGenericDal<T> repository)
		{
			_repository = repository;
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
		{
			return await _repository.AnyAsync(expression);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _repository.GetAll().ToListAsync();
		}

		public async Task<T> GetByIdAsync(Guid id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
		{
			return await _repository.GetSingleAsync(method, tracking);
		}

		public async Task<T> InsertAsync(T entity)
		{
			await _repository.InsertAsync(entity);
			return entity;
		}

		public void Remove(Guid id)
		{
			_repository.Remove(id);
		}

		public void Update(T entity)
		{
			_repository.Update(entity);
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			return _repository.Where(expression);
		}
	}
}
