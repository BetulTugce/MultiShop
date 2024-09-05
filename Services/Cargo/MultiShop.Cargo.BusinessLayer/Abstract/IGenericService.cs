using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
	public interface IGenericService<T> where T : class
	{
		// IGenericDal'daki methodlar..

		#region Read Methodları

		Task<T> GetByIdAsync(Guid id);

		Task<IEnumerable<T>> GetAllAsync();

		IQueryable<T> Where(Expression<Func<T, bool>> expression);

		Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

		// Belirtilen koşulu sağlayan ilk öğeyi asenkron olarak getirir.
		Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);

		#endregion

		#region Write Methodları

		Task<T> InsertAsync(T entity);

		void Update(T entity);

		void Remove(Guid id);

		#endregion
	}
}
