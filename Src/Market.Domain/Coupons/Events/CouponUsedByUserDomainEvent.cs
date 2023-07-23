using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Coupons.Events;
public class CouponUsedByUserDomainEvent : DomainEventBase
{
    public UserId UserId { get; private set; }
    public CouponId CouponId { get; private set; }

    public CouponUsedByUserDomainEvent(UserId userId, CouponId couponId)
    {
        UserId = userId;
        CouponId = couponId;
    }

}
