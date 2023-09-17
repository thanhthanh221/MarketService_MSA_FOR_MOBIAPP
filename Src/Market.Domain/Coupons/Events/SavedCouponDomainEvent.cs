using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Coupons.Events;

public class SavedCouponDomainEvent : DomainEventBase
{
    public UserId UserId { get; private set; }
    public CouponId CouponId { get; private set; }
    public int Amount { get; private set; }
    public SavedCouponDomainEvent(UserId userId, CouponId couponId, int amount)
    {
        UserId = userId;
        CouponId = couponId;
        Amount = amount;
    }
}
