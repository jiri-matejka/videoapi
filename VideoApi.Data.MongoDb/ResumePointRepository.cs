using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using VideoApi.Data.Entities;

namespace VideoApi.Data.MongoDb
{
	public class ResumePointRepository : IResumePointRepository
	{
		private readonly IMongoDatabase database;

		public ResumePointRepository(IMongoDatabase database)
		{
			this.database = database;
		}

		public ResumePoint Get(string id)
		{
			return null;
		}

		public async Task<IReadOnlyList<ResumePoint>> GetAll(string accountId)
		{
			IMongoCollection<Account> collection = this.database.GetCollection<Account>("accounts");

			FilterDefinition<Account> accountFilter = new BsonDocument(
				new BsonElement("_id", new BsonObjectId(new ObjectId(accountId))));

			IList<Account> accounts = await collection.Find(accountFilter).ToListAsync();
			if (accounts == null || accounts.Count == 0)
				throw new ArgumentException($"Account {accountId} does not exist");
			if (accounts.Count > 1)
				throw new InvalidOperationException($"There are more accounts with the same id {accountId}");

			return accounts[0].ResumePoints;
		}
	}
}
