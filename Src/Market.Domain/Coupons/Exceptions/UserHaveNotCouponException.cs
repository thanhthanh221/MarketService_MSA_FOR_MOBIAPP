namespace Market.Domain.Coupons.Exceptions;
public class UserHaveNotCouponException : Exception
{
    public UserHaveNotCouponException() : base("User does not have this coupon")
    {
    }
}
