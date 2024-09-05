using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.Abstract
{
	public interface IGenericDal<T> where T : class
	{
		#region Read Methodları

		// Belirli bir nesnenin, kimliği (IDsi) üzerinden asenkron olarak getirilmesini sağlar.
		Task<T> GetByIdAsync(Guid id);

		// Tüm nesnelerin getirilmesini sağlar.
		IQueryable<T> GetAll();

		// Belirli bir koşula uyan nesneleri getirir.
		IQueryable<T> Where(Expression<Func<T, bool>> expression);

		// Belirli bir koşula uyan nesne var mı yok mu kontrolünü asenkron olarak yapar.
		Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

		// Belirtilen koşulu sağlayan ilk öğeyi asenkron olarak getirir.
		Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
		#endregion


		#region Write Methodları

		// Belirli bir nesnenin eklenmesini asenkron olarak sağlar.
		Task InsertAsync(T entity);

		// Bir nesnenin güncellenmesini sağlar.
		//bool Update(T entity);
		void Update(T entity);

		// Belirli bir nesnenin silinmesini sağlar.
		//void Remove(T entity);
		void Remove(Guid id);

		#endregion
	}
}
