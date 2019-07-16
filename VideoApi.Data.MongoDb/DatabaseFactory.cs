using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using VideoApi.Data.MongoDb.Mapping;

namespace VideoApi.Data.MongoDb
{
    public static class DatabaseFactory
    {
		public static MongoClient CreateClient(IConfiguration configuration)
        {
			AccountSerializer.MapClasses();

			var databaseSection = configuration.GetSection("Database");
			string fullConnectionString = databaseSection["ConnectionString"];
			var mongoClient = new MongoClient(fullConnectionString);

			return mongoClient;
        }
    }
}
