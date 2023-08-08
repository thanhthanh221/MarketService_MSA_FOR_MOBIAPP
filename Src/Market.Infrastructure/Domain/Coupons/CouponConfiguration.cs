using Market.Domain.Coupons;
using MongoDB.Bson.Serialization;

namespace Market.Infrastructure.Domain.Coupons;
public static class CouponConfiguration
{
    public static void Configure(BsonClassMap<CouponAggregate> p)
    {
        p.AutoMap();
        p.MapIdMember(p => p.CouponId);
    }
}
