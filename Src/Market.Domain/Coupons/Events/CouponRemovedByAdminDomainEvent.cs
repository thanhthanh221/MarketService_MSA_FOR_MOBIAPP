using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Coupons.Events;

public class CouponRemovedByAdminDomainEvent : DomainEventBase
{
    public UserId AdminId { get; private set; }
    public CouponId CouponId { get; private set; }
    public CouponRemovedByAdminDomainEvent(UserId adminId, CouponId couponId)
    {
        AdminId = adminId;
        CouponId = couponId;
    }
}
