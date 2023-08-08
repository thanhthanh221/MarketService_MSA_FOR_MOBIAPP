using Market.Domain.Users;
using MongoDB.Bson.Serialization;

namespace Market.Infrastructure.Domain.Users;
public static class UserConfiguration
{
    public static void Configure(BsonClassMap<UserAggregate> u){
        u.AutoMap();
        u.SetIgnoreExtraElements(true);
        // // Key
        u.MapIdMember(x => x.UserId);

    }
}
