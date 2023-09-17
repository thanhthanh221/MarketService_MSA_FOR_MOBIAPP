using Market.Application.Contracts;

namespace Market.Application.Coupons.Commands.SaveCoupon;
public class SaveCouponCommand : CommandBase<Guid>
{
    public Guid CouponId { get; private set; }
    public Guid UserId { get; private set; }
    public SaveCouponCommand(Guid couponId, Guid userId)
    {
        CouponId = couponId;
        UserId = userId;
    }
}
