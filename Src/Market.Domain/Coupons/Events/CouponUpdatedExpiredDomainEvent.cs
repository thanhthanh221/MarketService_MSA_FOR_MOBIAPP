using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Coupons.Events;
public class CouponUpdatedExpiredDomainEvent : DomainEventBase
{
    public CouponId CouponId { get; private set; }
    public UserId UserId { get; private set; }

    public CouponUpdatedExpiredDomainEvent(CouponId couponId, UserId userId)
    {
        CouponId = couponId;
        UserId = userId;
    }

}
