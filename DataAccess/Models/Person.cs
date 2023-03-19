using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Models;

public class Person
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement]
    public string FirstName { get; set; } = string.Empty;

    [BsonElement]
    public string LastName { get; set; } = string.Empty;

    [BsonElement]
    public DateTime DateOfBirth { get; set; }
}