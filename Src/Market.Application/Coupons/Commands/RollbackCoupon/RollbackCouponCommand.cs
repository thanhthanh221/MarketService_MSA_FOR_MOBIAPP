using Market.Application.Contracts;

namespace Market.Application.Coupons.Commands.RollbackCoupon;
public class RollbackCouponCommand : CommandBase<bool>
{
    public Guid CouponId { get; private set; }
    public Guid UserId { get; private set; }
    public RollbackCouponCommand(Guid couponId, Guid userId)
    {
        CouponId = couponId;
        UserId = userId;
    }

}
