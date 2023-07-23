using MongoDB.Bson.Serialization;

namespace Market.Infrastructure.MongoDb;
public abstract class MongoDbConfigurationEntity<T> : BsonClassMap<T>
{
    public abstract void Configure();

}
