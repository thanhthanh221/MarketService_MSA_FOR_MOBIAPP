using Market.Application.Common.Cache;
using Market.Application.Configurations.Events;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Market.Application.Coupons.Events;
public class RecoveredCouponEventHandler : IEventHandler<RecoveredCouponDomainEvent>
{
    private readonly ICouponEventStore couponEventStore;
    private readonly IReposeCache reposeCache;
    private readonly ILogger<RemovedCouponEventHandler> logger;

    public RecoveredCouponEventHandler(
        ICouponEventStore couponEventStore, IReposeCache reposeCache, ILogger<RemovedCouponEventHandler> logger)
    {
        this.couponEventStore = couponEventStore;
        this.reposeCache = reposeCache;
        this.logger = logger;
    }

    public async Task Handle(RecoveredCouponDomainEvent @event, CancellationToken cancellationToken)
    {
        await couponEventStore.SaveDomainEventAsync(@event.CouponId, @event, cancellationToken);

        var cacheKey = CachePatternData.CouponPattern + @event.CouponId.Id;
        var couponDataInCache = await reposeCache.GetCacheReponseAsync(cacheKey);

        if (string.IsNullOrWhiteSpace(couponDataInCache))
        {
            var coupon = JsonConvert.DeserializeObject<CouponSnapshot>(couponDataInCache);

            coupon.CouponUsers.Add(new(@event.UserId));
            coupon.CountCouponUse--;

            await reposeCache.UpdateDataCacheAsync(cacheKey, coupon);
            logger.LogWarning($"Recovered Coupon: {coupon.CouponId} In Cache");
        }
    }
}
