using Market.Application.Common.Cache;
using Market.Application.Configurations.Events;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using Newtonsoft.Json;

namespace Market.Application.Coupons.Events;
public class UsedCouponEventHandler : IEventHandler<UsedCouponDomainEvent>
{
    private readonly ICouponEventStore couponEventStore;
    private readonly IReposeCache reposeCache;

    public UsedCouponEventHandler(
        ICouponEventStore couponEventStore, IReposeCache reposeCache)
    {
        this.couponEventStore = couponEventStore;
        this.reposeCache = reposeCache;
    }

    public async Task Handle(UsedCouponDomainEvent @event, CancellationToken cancellationToken)
    {
        await couponEventStore.SaveDomainEventAsync(@event.CouponId, @event, cancellationToken);
        var cacheKey = CachePatternData.CouponPattern + @event.CouponId.Id;

        var couponDataInCache = await reposeCache.GetCacheReponseAsync(cacheKey);
        
        if(!string.IsNullOrWhiteSpace(couponDataInCache)) {
            var coupon = JsonConvert.DeserializeObject<CouponSnapshot>(couponDataInCache);

            coupon.CouponUsers.ToHashSet().RemoveWhere(c => c.UserId.Equals(@event.UserId));
            coupon.CountCouponUse++;
            
            await reposeCache.UpdateDataCacheAsync(cacheKey, coupon);
        }
    }
}
