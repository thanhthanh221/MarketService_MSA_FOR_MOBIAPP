using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Coupons.Events;
public class CouponRecoveredDomainEvent : DomainEventBase
{
    public CouponId CouponId { get; private set; }
    public UserId UserId { get; private set; }
    public int CountCouponUse { get; private set; }

    public CouponRecoveredDomainEvent(CouponId couponId, UserId userId, int countCouponUse)
    {
        CouponId = couponId;
        UserId = userId;
        CountCouponUse = countCouponUse;
    }
}
