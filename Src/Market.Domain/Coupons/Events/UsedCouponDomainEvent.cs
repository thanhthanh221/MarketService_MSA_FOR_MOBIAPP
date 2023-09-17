using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Coupons.Events;
public class UsedCouponDomainEvent : DomainEventBase
{
    public UserId UserId { get; private set; }
    public CouponId CouponId { get; private set; }
    public int CountCouponUse { get; private set; }
    public UsedCouponDomainEvent(UserId userId, CouponId couponId, int countCouponUse)
    {
        UserId = userId;
        CouponId = couponId;
        CountCouponUse = countCouponUse;
    }
}
