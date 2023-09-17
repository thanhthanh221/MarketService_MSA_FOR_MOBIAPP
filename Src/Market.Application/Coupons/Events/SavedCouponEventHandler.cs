using Market.Application.Common.Cache;
using Market.Application.Configurations.Events;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using Newtonsoft.Json;

namespace Market.Application.Coupons.Events;
public class SavedCouponEventHandler : IEventHandler<SavedCouponDomainEvent>
{
    private readonly ICouponEventStore couponEventStore;
    private readonly IReposeCache reposeCache;

    public SavedCouponEventHandler(
        ICouponEventStore couponEventStore, IReposeCache reposeCache)
    {
        this.couponEventStore = couponEventStore;
        this.reposeCache = reposeCache;
    }

    public async Task Handle(SavedCouponDomainEvent @event, CancellationToken cancellationToken)
    {
        await couponEventStore.SaveDomainEventAsync(@event.CouponId, @event, cancellationToken);

        var cacheKey = CachePatternData.CouponPattern + @event.CouponId.Id;
        var couponInCacheToString = await reposeCache.GetCacheReponseAsync(cacheKey);

        if (!string.IsNullOrWhiteSpace(couponInCacheToString))
        {
            var coupon = JsonConvert.DeserializeObject<CouponSnapshot>(couponInCacheToString);

            coupon.Amount--;
            coupon.CouponUsers.Add(new(@event.UserId));

            await reposeCache.UpdateDataCacheAsync(cacheKey, coupon);
        }
    }
}
