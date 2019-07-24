using System;
using System.Collections.Generic;
using System.Text;

namespace VideoApi.Data
{
	public interface IDatabaseConfigurationProvider
	{
		string MongoConnectionString { get; }
		string DatabaseName { get; }
	}
}
