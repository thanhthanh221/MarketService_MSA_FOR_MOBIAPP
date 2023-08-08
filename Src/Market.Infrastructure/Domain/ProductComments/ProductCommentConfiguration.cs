using Market.Domain.ProductComments;
using MongoDB.Bson.Serialization;

namespace Market.Infrastructure.Domain.ProductComments;
public static class ProductCommentConfiguration
{
    public static void Configure(BsonClassMap<ProductCommentAggregate> p)
    {
        p.AutoMap();
        p.SetIgnoreExtraElements(true);
        // // Key
        p.MapIdMember(x => x.ProductCommentId);
    }
}
