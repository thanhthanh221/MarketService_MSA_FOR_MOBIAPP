using Market.Application.Common.Cache;
using Market.Application.Configurations.Events;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;

namespace Market.Application.Coupons.Events;
public class CreatedCouponEventHandler : IEventHandler<CouponCreatedByAdminDomainEvent>
{
    private readonly ICouponEventStore couponEventStore;
    private readonly IReposeCache reposeCache;

    public CreatedCouponEventHandler(ICouponEventStore couponEventStore, IReposeCache reposeCache)
    {
        this.couponEventStore = couponEventStore;
        this.reposeCache = reposeCache;
    }

    public async Task Handle(CouponCreatedByAdminDomainEvent @event, CancellationToken cancellationToken)
    {
        await couponEventStore.SaveDomainEventAsync(@event.CouponAggregate);

        var cacheKey = CachePatternData.CouponPattern + @event.CouponAggregate.CouponId.Id;
        var couponShapshot = CouponSnapshot.ConverConponToSnapshot(@event.CouponAggregate);
        
        await reposeCache.SetCacheReponseAsync(cacheKey, couponShapshot, new TimeSpan(24, 0, 0));
    }
}
