
using Market.Application.Contracts;

namespace Market.Application.Coupons.Commands.UserSaveCoupon;

public class UserSaveCouponCommand : CommandBase<Guid>
{

    public Guid CouponId { get; private set; }
    public Guid UserId { get; private set; }
    public UserSaveCouponCommand(Guid couponId, Guid userId)
    {
        CouponId = couponId;
        UserId = userId;
    }
}
