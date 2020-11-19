using CryptoCore.Models.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbContext.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbContext.Services
{
	public class BaseMongoRepository<TModel>
		where TModel : MongoBaseModel
	{
		private readonly IMongoCollection<TModel> mongoCollection;

		public BaseMongoRepository(DbSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.Database);
			mongoCollection = database.GetCollection<TModel>(settings.Table);
		}

		public virtual async Task<TModel> GetFirstOrDefaultAsync()
		{
			return await mongoCollection.Find(m => true).FirstOrDefaultAsync();
		}

		public virtual async Task<List<TModel>> GetListAsync()
		{
			return await mongoCollection.Find(m => true).ToListAsync();
		}

		public virtual async Task<TModel> GetByIdAsync(string id)
		{
			return await mongoCollection.Find<TModel>(m => m.Id == id).FirstOrDefaultAsync();
		}

		public virtual async Task<TModel> CreateAsync(TModel model)
		{
			await mongoCollection.InsertOneAsync(model);
			return model;
		}

		public virtual async Task InsertManyAsync(List<TModel> model)
		{
			await mongoCollection.InsertManyAsync(model);
		}

		public virtual async Task UpdateAsync(string id, TModel model)
		{
			await mongoCollection.ReplaceOneAsync(m => m.Id == id, model);
		}

		public virtual async Task DeleteAsync(TModel model)
		{
			await mongoCollection.DeleteOneAsync(m => m.Id == model.Id);
		}

		public virtual async Task DeleteAsync(string id)
		{
			await mongoCollection.DeleteOneAsync(m => m.Id == id);
		}

		public virtual async Task<DeleteResult> DeleteManyAsync()
		{
			return await mongoCollection.DeleteManyAsync(new BsonDocument());
		}
	}
}