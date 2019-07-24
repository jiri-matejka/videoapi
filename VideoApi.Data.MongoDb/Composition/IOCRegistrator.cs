using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoApi.Data.MongoDb.Mapping;

namespace VideoApi.Data.MongoDb.Composition
{
	public static class IOCRegistrator
	{
		public static void Register(IServiceCollection services)
		{
			services.AddSingleton<MappingConfigurator>();
			services.AddSingleton<DatabaseFactory>();

			services.AddSingleton(
				(provider) =>
					provider.GetService<DatabaseFactory>().GetDefaultDatabase()
				);

			// Resume point repository does not have any per-scope state,
			// because IMongoCollection itself can be singleton
			services.AddSingleton<IResumePointRepository, ResumePointRepository>();

		}
	}
}
