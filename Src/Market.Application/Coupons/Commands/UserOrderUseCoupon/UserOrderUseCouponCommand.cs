using Market.Application.Contracts;

namespace Market.Application.Coupons.Commands.UserOrderUseCoupon;
public class UserOrderUseCouponCommand : CommandBase<bool>
{
    public Guid CouponId { get; private set; }
    public Guid UserId { get; private set; }
    public UserOrderUseCouponCommand(Guid couponId, Guid userId)
    {
        CouponId = couponId;
        UserId = userId;
    }

}
