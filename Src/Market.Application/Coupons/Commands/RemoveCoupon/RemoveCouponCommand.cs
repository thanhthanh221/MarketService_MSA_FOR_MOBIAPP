using Market.Application.Contracts;

namespace Market.Application.Coupons.Commands.RemoveCoupon;
public class RemoveCouponCommand : CommandBase<Guid>
{
    public Guid CouponId { get; private set; }
    public Guid AdminId { get; private set; }
    public RemoveCouponCommand(Guid couponId)
    {
        CouponId = couponId;
    }

}
