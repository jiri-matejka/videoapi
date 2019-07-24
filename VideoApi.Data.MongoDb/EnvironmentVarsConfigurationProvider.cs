using System;
using System.Collections.Generic;
using System.Text;
using VideoApi.Common;

namespace VideoApi.Data
{
	public class EnvironmentVarsConfigurationProvider : IDatabaseConfigurationProvider
	{
		public string MongoConnectionString
		{
			get
			{
				// mongodb://root:admin@mongo:27017/admin
				string user = Environment.GetEnvironmentVariable("VIDEOAPI_MONGO_USER");
				string password = Environment.GetEnvironmentVariable("VIDEOAPI_MONGO_PWD");
				string host = Environment.GetEnvironmentVariable("VIDEOAPI_MONGO_HOST");
				string port = Environment.GetEnvironmentVariable("VIDEOAPI_MONGO_PORT");
				string authdb = Environment.GetEnvironmentVariable("VIDEOAPI_MONGO_AUTHDB");

				if (user.IsNullOrEmpty() || password.IsNullOrEmpty() || host.IsNullOrEmpty() ||
					port.IsNullOrEmpty())
					throw new InvalidOperationException("Some configuration values are not set");

				string result = $"mongodb://{user}:{password}@{host}:{port}/{authdb}";
				return result;
			}
		}

		public string DatabaseName
		{
			get
			{
				string name = Environment.GetEnvironmentVariable("VIDEOAPI_MONGO_DB");

				if(name.IsNullOrEmpty())
					throw new InvalidOperationException("Configuration value VIDEOAPI_MONGO_DB is not set");

				return name;
			}
		}
	}
}
