using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Coupons.Events;

public class CouponSavedByUserDomainEvent : DomainEventBase
{
    public UserId UserId { get; private set; }
    public CouponId CouponId { get; private set; }
    public int Amount { get; private set; }
    public int UserSaveCouponCount { get; private set; }

    public CouponSavedByUserDomainEvent(UserId userId, CouponId couponId, int amount, int userSaveCouponCount)
    {
        UserId = userId;
        CouponId = couponId;
        Amount = amount;
        UserSaveCouponCount = userSaveCouponCount;
    }

}
