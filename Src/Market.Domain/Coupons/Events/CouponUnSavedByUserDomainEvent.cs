using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Coupons.Events;
public class CouponUnSavedByUserDomainEvent : DomainEventBase
{
    public UserId UserId { get; private set; }
    public CouponId CouponId { get; private set; }
    public int Amount { get; private set; }
    public int UserSaveCouponCount {get; private set;}

    public CouponUnSavedByUserDomainEvent(UserId userId, CouponId couponId, int amount, int userSaveCouponCount)
    {
        UserId = userId;
        CouponId = couponId;
        Amount = amount;
        UserSaveCouponCount = userSaveCouponCount;
    }
}
