using Market.Domain.Users;

namespace Market.Domain.Coupons;

public class CouponUser
{
    public UserId UserId { get; set; }
    public DateTime? TimeUserSaveCoupon { get; set; }

    public CouponUser(UserId userId)
    {
        UserId = userId;
        TimeUserSaveCoupon = DateTime.UtcNow;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        CouponUser otherObject = (CouponUser)obj;

        return UserId.Id == otherObject.UserId.Id;
    }

    public override int GetHashCode()
    {
        return $"{UserId}".GetHashCode();
    }

}
