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

		public async Task<ResumePoint> Get(string accountId, string videoId)
		{
			IMongoCollection<Account> collection = this.database.GetCollection<Account>("accounts");

			FilterDefinition<Account> filter = 
				Builders<Account>.Filter.Eq("_id", new ObjectId(accountId)) &
				Builders<Account>.Filter.ElemMatch(a => a.ResumePoints, rp => rp.VideoId == new BsonObjectId(new ObjectId(videoId)));

			var result = await collection.Find(filter).SingleOrDefaultAsync();

			return result.ResumePoints.SingleOrDefault();
		}

		public async Task<IReadOnlyList<ResumePoint>> GetAll(string accountId)
		{
			IMongoCollection<Account> collection = this.database.GetCollection<Account>("accounts");

			FilterDefinition<Account> accountFilter = new BsonDocument(
				new BsonElement("_id", new BsonObjectId(new ObjectId(accountId))));

			Account account = await collection.Find(accountFilter).FirstOrDefaultAsync();
			if (account == null)
				throw new ArgumentException($"Account {accountId} does not exist");

			return account.ResumePoints;
		}
	}
}
