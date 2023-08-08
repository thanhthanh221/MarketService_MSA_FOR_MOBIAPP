using Market.Domain.Core;

namespace Market.Domain.Coupons;
public interface ICouponEventStore
{
    Task SaveDomainEventAsync(CouponAggregate couponAggregate);
    Task SaveDomainEventAsync(CouponId couponId, object domainEvent,
        CancellationToken cancellationToken = default);
    Task<CouponAggregate> GetDomainEventAsync(CouponId couponId, CancellationToken cancellationToken = default);
}
