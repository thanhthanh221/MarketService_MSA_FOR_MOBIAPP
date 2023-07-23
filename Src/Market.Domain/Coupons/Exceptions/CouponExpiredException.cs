namespace Market.Domain.Coupons.Exceptions;
public class CouponExpiredException : Exception
{
    public CouponExpiredException() : base("Coupon has expired")
    {
    }
}
