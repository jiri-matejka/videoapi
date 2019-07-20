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
		private readonly IMongoCollection<Account> accountCollection;
		private readonly IMongoCollection<ResumePoint> pointCollection;

		public ResumePointRepository(IMongoDatabase database)
		{
			this.database = database;
			this.accountCollection = this.database.GetCollection<Account>("accounts");
			this.pointCollection = this.database.GetCollection<ResumePoint>("resumePoints");
		}

		public async Task<ResumePoint> Get(string accountId, string videoId)
		{
			FilterDefinition<Account> filter =
				Builders<Account>.Filter.Eq("_id", new ObjectId(accountId)) &
				Builders<Account>.Filter.ElemMatch(a => a.ResumePoints, rp => rp.VideoId == new BsonObjectId(new ObjectId(videoId)));

			var result = await this.accountCollection.Find(filter).SingleOrDefaultAsync();

			return result.ResumePoints.SingleOrDefault();
		}

		public async Task<IReadOnlyList<ResumePoint>> GetAll(string accountId)
		{
			FilterDefinition<Account> accountFilter = new BsonDocument(
				new BsonElement("_id", new BsonObjectId(new ObjectId(accountId))));

			Account account = await this.accountCollection.Find(accountFilter).FirstOrDefaultAsync();
			if (account == null)
				throw new ArgumentException($"Account {accountId} does not exist");

			return account.ResumePoints;
		}

		public async Task InsertOrUpdate(string accountId, string videoId, double timePoint)
		{
			FilterDefinition<ResumePoint> filter = new BsonDocument()
			{
				{ "accountId", accountId },
				{ "videoId", videoId }
			};

			UpdateDefinition<ResumePoint> update = 
				new BsonDocument("$set", new BsonDocument("timePoint", timePoint));

			UpdateResult result = await this.pointCollection.UpdateOneAsync(
				filter, update, new UpdateOptions { IsUpsert = true });
			
		}
	}
}
