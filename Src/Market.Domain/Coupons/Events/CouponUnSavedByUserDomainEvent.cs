using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Coupons.Events;
public class CouponUnSavedByUserDomainEvent : DomainEventBase
{
    public UserId UserId { get; private set; }
    public CouponId CouponId { get; private set; }
    public CouponUnSavedByUserDomainEvent(UserId userId, CouponId couponId)
    {
        UserId = userId;
        CouponId = couponId;
    }
}
