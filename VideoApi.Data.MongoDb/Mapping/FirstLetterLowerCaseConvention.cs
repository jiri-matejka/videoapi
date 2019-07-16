using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace VideoApi.Data.MongoDb.Mapping
{
	public class FirstLetterLowerCaseConvention : IMemberMapConvention
	{
		public string Name => nameof(FirstLetterLowerCaseConvention);

		public void Apply(BsonMemberMap memberMap)
		{
			string csharpName = memberMap.MemberName;
			string mongoName = csharpName.Substring(0, 1).ToLower() + csharpName.Substring(1);
			memberMap.SetElementName(mongoName);
		}
	}
}
