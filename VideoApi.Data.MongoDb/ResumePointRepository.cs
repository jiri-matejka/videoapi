using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using VideoApi.Data.Entities;
using VideoApi.Data.MongoDb.Mapping;

namespace VideoApi.Data.MongoDb
{
	public class ResumePointRepository : IResumePointRepository
	{
		private readonly IMongoDatabase database;
		private readonly MappingConfigurator mappingConfigurator;
		private readonly IMongoCollection<ResumePoint> pointCollection;

		private readonly string accountIdColumnName;
		private readonly string videoIdColumnName;
		private readonly string timePointColumnName;

		public ResumePointRepository(IMongoDatabase database, MappingConfigurator mappingConfigurator)
		{
			this.database = database;
			this.mappingConfigurator = mappingConfigurator;
			this.pointCollection = this.database.GetCollection<ResumePoint>("resumePoints");

			accountIdColumnName = this.mappingConfigurator.MapNameToMongo(nameof(ResumePoint.AccountId));
			videoIdColumnName = this.mappingConfigurator.MapNameToMongo(nameof(ResumePoint.VideoId));
			timePointColumnName = this.mappingConfigurator.MapNameToMongo(nameof(ResumePoint.TimePoint));
		}

		public async Task<ResumePoint> Get(string accountId, string videoId)
		{
			FilterDefinition<ResumePoint> filter =
				Builders<ResumePoint>.Filter.Eq(accountIdColumnName, new ObjectId(accountId)) &
				Builders<ResumePoint>.Filter.Eq(videoIdColumnName, new ObjectId(videoId));

			var result = await this.pointCollection.Find(filter).SingleOrDefaultAsync();

			return result;
		}

		public async Task<IReadOnlyList<ResumePoint>> GetAll(string accountId)
		{
			FilterDefinition<ResumePoint> filter = new BsonDocument(
				new BsonElement(accountIdColumnName, new BsonObjectId(new ObjectId(accountId))));

			IReadOnlyList<ResumePoint> list = await this.pointCollection.Find(filter).ToListAsync();
			
			return list;
		}

		public async Task InsertOrUpdate(string accountId, string videoId, double timePoint)
		{
			FilterDefinition<ResumePoint> filter = new BsonDocument()
			{
				{ accountIdColumnName, accountId },
				{ videoIdColumnName, videoId }
			};

			UpdateDefinition<ResumePoint> update = 
				new BsonDocument("$set", new BsonDocument(timePointColumnName, timePoint));

			UpdateResult result = await this.pointCollection.UpdateOneAsync(
				filter, update, new UpdateOptions { IsUpsert = true });
			
		}
	}
}
