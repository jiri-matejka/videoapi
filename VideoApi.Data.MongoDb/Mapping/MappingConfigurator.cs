using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;
using VideoApi.Data.Entities;

namespace VideoApi.Data.MongoDb.Mapping
{
	internal class MappingConfigurator
    {
		public static void ConfigureMapping()
        {
			var pack = new ConventionPack();
			pack.Add(new FirstLetterLowerCaseConvention());
			ConventionRegistry.Register(
				nameof(FirstLetterLowerCaseConvention),
				pack,
				t => t.FullName.StartsWith("VideoApi.Data"));

			BsonClassMap.RegisterClassMap<Account>(cm =>
            {
                cm.AutoMap();
				cm.MapProperty(a => a.Id).SetSerializer(new ObjectIdToStringSerializer());				
            });

			BsonClassMap.RegisterClassMap<ResumePoint>(cm =>
			{
				cm.AutoMap();
				cm.MapProperty(r => r.VideoId).SetSerializer(new ObjectIdToStringSerializer());
			});
		}

    }
}
