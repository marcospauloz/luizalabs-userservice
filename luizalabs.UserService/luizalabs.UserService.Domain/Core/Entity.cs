using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace luizalabs.UserService.Domain.Core;

public abstract class Entity
{
    protected Entity()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; }
}