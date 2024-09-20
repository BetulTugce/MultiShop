namespace MultiShop.Catalog.Repositories.Abstractions
{
	public interface IGenericRepository<T> where T : class
	{
		// Temel CRUD işlemleri..
		Task<List<T>> GetAllAsync();
		Task<T> GetByIdAsync(string id);
		Task CreateAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(string id);
		Task DeleteAllAsync();
	}
}
