using Market.Application.Contracts;

namespace Market.Application.Coupons.Commands.UserUnSaveCoupon;
public class UserUnSaveCouponCommand : CommandBase<Guid>
{
    public Guid CouponId { get; private set; }
    public Guid UserId { get; private set; }
    public UserUnSaveCouponCommand(Guid couponId, Guid userId)
    {
        CouponId = couponId;
        UserId = userId;
    }
}
