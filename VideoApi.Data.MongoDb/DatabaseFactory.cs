using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace VideoApi.Data.MongoDb
{
    public static class DatabaseFactory
    {
		public static MongoClient CreateClient(IConfiguration configuration)
        {
			var databaseSection = configuration.GetSection("Database");
			string fullConnectionString = databaseSection["ConnectionString"];
			return new MongoDB.Driver.MongoClient(fullConnectionString);
        }
    }
}
