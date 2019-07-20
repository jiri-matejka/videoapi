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
			services.AddScoped<IResumePointRepository, ResumePointRepository>();
			
			services.AddSingleton<MappingConfigurator>();
			services.AddSingleton<DatabaseFactory>();

			services.AddSingleton(
				(provider) =>
					provider.GetService<DatabaseFactory>().GetDefaultDatabase()
				);
		}
	}
}
