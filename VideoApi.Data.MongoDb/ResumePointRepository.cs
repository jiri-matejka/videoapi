using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using VideoApi.Data.Entities;
using VideoApi.Data.MongoDb.Mapping;
using VideoApi.Common;
using VideoApi.Common.Exceptions;

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

			EnsureIndexCreated();
		}

		private void EnsureIndexCreated()
		{
			// We create multikey index {accountId, videoId} for fast queries
			// for accountId or accountId+videoId.
			// Since this class is singleton in IoC, we can afford to create them 
			// once at the first request, synchronously.
			// If the index is already existing, Mongo does nothing.
			pointCollection.Indexes.CreateOne(new CreateIndexModel<ResumePoint>(
				new IndexKeysDefinitionBuilder<ResumePoint>()
					.Ascending(x => x.AccountId)
					.Ascending(x => x.VideoId)));
		}

		public async Task<ResumePoint> Get(string accountId, string videoId)
		{
			if (accountId.IsNullOrEmpty() || !ObjectId.TryParse(accountId, out ObjectId _))
				throw new ValidationException($"{nameof(accountId)} is not a correct ObjectId");
			if (videoId.IsNullOrEmpty() || !ObjectId.TryParse(videoId, out ObjectId _))
				throw new ValidationException($"{nameof(videoId)} is not a correct ObjectId");
			
			FilterDefinition<ResumePoint> filter =
				Builders<ResumePoint>.Filter.Eq(accountIdColumnName, new ObjectId(accountId)) &
				Builders<ResumePoint>.Filter.Eq(videoIdColumnName, new ObjectId(videoId));

			var result = await this.pointCollection.Find(filter).SingleOrDefaultAsync();

			return result;
		}

		public async Task<IReadOnlyList<ResumePoint>> GetAll(string accountId)
		{
			if (accountId.IsNullOrEmpty() || !ObjectId.TryParse(accountId, out ObjectId _))
				throw new ValidationException($"{nameof(accountId)} is not a correct ObjectId");

			FilterDefinition<ResumePoint> filter = new BsonDocument(
				new BsonElement(accountIdColumnName, new BsonObjectId(new ObjectId(accountId))));

			IReadOnlyList<ResumePoint> list = await this.pointCollection.Find(filter).ToListAsync();
			
			return list;
		}

		public async Task InsertOrUpdate(string accountId, string videoId, double timePoint)
		{
			if (accountId.IsNullOrEmpty() || !ObjectId.TryParse(accountId, out ObjectId _))
				throw new ValidationException($"{nameof(accountId)} is not a correct ObjectId");
			if (videoId.IsNullOrEmpty() || !ObjectId.TryParse(videoId, out ObjectId _))
				throw new ValidationException($"{nameof(videoId)} is not a correct ObjectId");
			if (timePoint < 0)
				throw new ValidationException($"{nameof(timePoint)} must be non-negative number");

			FilterDefinition<ResumePoint> filter = new BsonDocument()
			{
				{ accountIdColumnName, new BsonObjectId(new ObjectId(accountId)) },
				{ videoIdColumnName, new BsonObjectId(new ObjectId(videoId)) }
			};

			UpdateDefinition<ResumePoint> update = 
				new BsonDocument("$set", new BsonDocument(timePointColumnName, timePoint));

			UpdateResult result = await this.pointCollection.UpdateOneAsync(
				filter, update, new UpdateOptions { IsUpsert = true });
			
		}
	}
}
