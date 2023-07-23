using Market.Domain.Core;

namespace Market.Domain.Coupons.Events;

public class CouponCreatedByAdminDomainEvent : DomainEventBase
{
    public CouponAggregate CouponAggregate { get; private set; }
    public CouponCreatedByAdminDomainEvent(CouponAggregate couponAggregate)
    {
        CouponAggregate = couponAggregate;
    }
}
