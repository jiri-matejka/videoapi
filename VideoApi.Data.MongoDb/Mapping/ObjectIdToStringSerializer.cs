using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace VideoApi.Data.MongoDb.Mapping
{
	internal partial class AccountSerializer
    {
		private class ObjectIdToStringSerializer : SerializerBase<string>
		{
			public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
			{
				return context.Reader.ReadObjectId().ToString();
			}

			public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
			{
				context.Writer.WriteObjectId(new MongoDB.Bson.ObjectId(value));
			}

		}

    }
}
