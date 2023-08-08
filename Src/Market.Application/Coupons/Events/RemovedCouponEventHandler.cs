using Market.Application.Common.Cache;
using Market.Application.Configurations.Events;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using Microsoft.Extensions.Logging;

namespace Market.Application.Coupons.Events;
public class RemovedCouponEventHandler : IEventHandler<CouponRemovedByAdminDomainEvent>
{
    private readonly ICouponEventStore couponEventStore;
    private readonly IReposeCache reposeCache;
    private readonly ILogger<RemovedCouponEventHandler> logger;

    public RemovedCouponEventHandler(
        ICouponEventStore couponEventStore, IReposeCache reposeCache, ILogger<RemovedCouponEventHandler> logger)
    {
        this.couponEventStore = couponEventStore;
        this.reposeCache = reposeCache;
        this.logger = logger;
    }

    public async Task Handle(CouponRemovedByAdminDomainEvent @event, CancellationToken cancellationToken)
    {
        await couponEventStore.SaveDomainEventAsync(@event.CouponId, @event, cancellationToken);

        var couponDataInCache = await reposeCache.GetCacheReponseAsync(CachePatternData.CouponPattern + @event.CouponId.Id);

        if (!string.IsNullOrWhiteSpace(couponDataInCache))
        {

            await reposeCache.RemoveCacheAsync(CachePatternData.CouponPattern, @event.CouponId.Id);

            logger.LogWarning($"Remove Coupon: {@event.CouponId.Id} In Cache");
        }
    }
}
