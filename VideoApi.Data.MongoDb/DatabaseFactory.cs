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
		private readonly IDatabaseConfigurationProvider configuration;
		private readonly MappingConfigurator mappingConfigurator;
		private readonly IMongoClient mongoClient;

		public DatabaseFactory(IDatabaseConfigurationProvider configuration, MappingConfigurator mappingConfigurator)
		{
			this.configuration = configuration;
			this.mappingConfigurator = mappingConfigurator;

			this.mappingConfigurator.ConfigureMapping();

			this.mongoClient = CreateClient();
		}

		private MongoClient CreateClient()
        {
			string fullConnectionString = this.configuration.MongoConnectionString;
			var mongoClient = new MongoClient(fullConnectionString);
			
			return mongoClient;
        }

		public IMongoDatabase GetDatabase()
		{
			string databaseName = this.configuration.DatabaseName;
			IMongoDatabase db = mongoClient.GetDatabase(databaseName);

			return db;
		}
    }
}
