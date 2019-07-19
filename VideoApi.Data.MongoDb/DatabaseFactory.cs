using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using VideoApi.Data.MongoDb.Mapping;

namespace VideoApi.Data.MongoDb
{
    public class DatabaseFactory
    {
		private readonly IConfiguration configuration;

		public DatabaseFactory(IConfiguration configuration)
		{
			MappingConfigurator.ConfigureMapping();

			this.configuration = configuration;
		}

		public MongoClient CreateClient()
        {
			var databaseSection = this.configuration.GetSection("Database");
			string fullConnectionString = databaseSection["ConnectionString"];
			var mongoClient = new MongoClient(fullConnectionString);
			
			return mongoClient;
        }

		public IMongoDatabase GetDefaultDatabase(MongoClient client)
		{
			var databaseSection = configuration.GetSection("Database");
			string databaseName = databaseSection["DatabaseName"];

			IMongoDatabase db = client.GetDatabase(databaseName);

			return db;
		}
    }
}
