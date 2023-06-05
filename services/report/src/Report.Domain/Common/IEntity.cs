using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Report.Domain.Common
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        Guid Id { get; set; }
    }
}
