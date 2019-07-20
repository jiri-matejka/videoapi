using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using VideoApi.Data.MongoDb.Mapping;

namespace VideoApi.Data.MongoDb
{
    internal class DatabaseFactory
    {
		private readonly IConfiguration configuration;
		private readonly MappingConfigurator mappingConfigurator;
		private readonly IMongoClient mongoClient;

		public DatabaseFactory(IConfiguration configuration, MappingConfigurator mappingConfigurator)
		{
			this.configuration = configuration;
			this.mappingConfigurator = mappingConfigurator;

			this.mappingConfigurator.ConfigureMapping();

			this.mongoClient = CreateClient();
		}

		private MongoClient CreateClient()
        {
			var databaseSection = this.configuration.GetSection("Database");
			string fullConnectionString = databaseSection["ConnectionString"];
			var mongoClient = new MongoClient(fullConnectionString);
			
			return mongoClient;
        }

		public IMongoDatabase GetDefaultDatabase()
		{
			var databaseSection = configuration.GetSection("Database");
			string databaseName = databaseSection["DatabaseName"];

			IMongoDatabase db = mongoClient.GetDatabase(databaseName);

			return db;
		}
    }
}
