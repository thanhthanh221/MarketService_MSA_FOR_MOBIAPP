using Market.Domain.Coupons;
using Market.Infrastructure.MarketContext;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Market.Infrastructure.Domain.Coupons;
public class CouponRepository : ICouponRepository
{
    private readonly IMongoCollection<CouponAggregate> couponCollection;
    private readonly FilterDefinitionBuilder<CouponAggregate> filterBuilder = Builders<CouponAggregate>.Filter;

    public CouponRepository(MarketDbContext context)
    {
        couponCollection = context.Coupons;
    }
    public async Task CreateCouponAsync(CouponAggregate couponAggregate)
    {
        await couponCollection.InsertOneAsync(couponAggregate);
    }

    public async Task<List<CouponAggregate>> GetAllCouponAsync()
    {
        return await couponCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<CouponAggregate> GetCouponByIdAsync(CouponId couponId)
    {
        var filter = filterBuilder?.Eq(p => p.CouponId, couponId);
        return await couponCollection.Find(filter).FirstOrDefaultAsync();
    }
    public async Task RemoveCouponAsync(CouponId couponId)
    {
        var filter = filterBuilder?.Eq(p => p.CouponId, couponId);
        await couponCollection.DeleteOneAsync(filter);
    }

    public async Task UpdateCouponAsync(CouponAggregate couponAggregate)
    {
        var filter = filterBuilder?.Eq(p => p.CouponId, couponAggregate.CouponId);
        await couponCollection.ReplaceOneAsync(filter, couponAggregate);
    }
}
