namespace Market.Domain.Coupons.Exceptions;
public class UserHadCouponException : Exception
{
    public UserHadCouponException() : base("User Had Coupon")
    {
    }
}
