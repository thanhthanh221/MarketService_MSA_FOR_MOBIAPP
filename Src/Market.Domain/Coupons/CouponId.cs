using Market.Domain.Core;
namespace Market.Domain.Coupons;
public class CouponId : TypedIdValueBase
{
    public CouponId(Guid value) : base(value)
    {
    }
}
