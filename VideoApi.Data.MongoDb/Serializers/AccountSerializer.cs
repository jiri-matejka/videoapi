using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;
using VideoApi.Data.Entities;

namespace VideoApi.Data.MongoDb.Serializers
{
    internal class AccountSerializer
    {
        public static void MapClasses()
        {
            BsonClassMap.RegisterClassMap<Account>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(a => a.Id);
                cm.IdMemberMap.SetIdGenerator(ObjectIdGenerator.Instance);
            });

            BsonClassMap.RegisterClassMap<ResumePoint>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}
