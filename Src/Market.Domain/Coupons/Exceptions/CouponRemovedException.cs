namespace Market.Domain.Coupons.Exceptions;
public class CouponRemovedException : Exception
{
    public CouponRemovedException() : base("Coupon has been removed")
    {
    }
}
