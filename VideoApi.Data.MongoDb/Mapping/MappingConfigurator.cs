using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;
using VideoApi.Data.Entities;

namespace VideoApi.Data.MongoDb.Mapping
{
	public class MappingConfigurator
    {
		private readonly FirstLetterLowerCaseConvention firstLetterLowerCaseConvention;

		public MappingConfigurator()
		{
			this.firstLetterLowerCaseConvention = new FirstLetterLowerCaseConvention();
		}

		public void ConfigureMapping()
        {
			var pack = new ConventionPack();
			pack.Add(this.firstLetterLowerCaseConvention);
			ConventionRegistry.Register(
				nameof(FirstLetterLowerCaseConvention),
				pack,
				t => t.FullName.StartsWith("VideoApi.Data"));

			BsonClassMap.RegisterClassMap<ResumePoint>(cm =>
			{
				cm.AutoMap();
				cm.SetIgnoreExtraElements(true);
				cm.MapProperty(r => r.VideoId).SetSerializer(new ObjectIdToStringSerializer());
				cm.MapProperty(r => r.AccountId).SetSerializer(new ObjectIdToStringSerializer());
			});
		}

		public string MapNameToMongo(string csharpName)
		{
			// simplistic version
			return this.firstLetterLowerCaseConvention.MapNameToMongo(csharpName);
		}

    }
}
