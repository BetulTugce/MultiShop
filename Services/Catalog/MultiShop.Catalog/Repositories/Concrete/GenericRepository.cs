using MongoDB.Driver;
using MultiShop.Catalog.Repositories.Abstractions;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Repositories.Concrete
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly IMongoCollection<T> _collection;

		public GenericRepository(IDatabaseSettings databaseSettings, string collectionName)
		{
			var client = new MongoClient(databaseSettings.ConnectionString);
			var database = client.GetDatabase(databaseSettings.DatabaseName);
			_collection = database.GetCollection<T>(collectionName);
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
		}

		public async Task<T> GetByIdAsync(string id)
		{
			return await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
		}

		public async Task CreateAsync(T entity)
		{
			await _collection.InsertOneAsync(entity);
		}

		public async Task UpdateAsync(T entity)
		{
			var id = entity.GetType().GetProperty("Id").GetValue(entity, null).ToString();
			await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);

			//await _collection.FindOneAndReplaceAsync(entity.GetType().GetProperty("Id").GetValue(entity).ToString(), entity);
		}

		public async Task DeleteAsync(string id)
		{
			await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
		}

        public async Task DeleteAllAsync()
        {
            // Tüm dökümanları silmek için boş bir filtre tanımlanıyor..
            var filter = Builders<T>.Filter.Empty;

			await _collection.DeleteManyAsync(filter);
        }
    }
}
