using Market.Domain.Core;

namespace Market.Domain.Coupons;
public class CouponStatus : Enumeration
{
    public static CouponStatus Activity { get; set; } = new(1, "Hoạt động");
    public static CouponStatus Expires { get; set; } = new(2, "Hết hạn");
    public static CouponStatus Deleted { get; set; } = new(3, "Đã xóa");
    public string Status { get; private set; }
    public CouponStatus(int id, string status)
    {
        Status = status;
        Id = id;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType()) {
            return false;
        }

        CouponStatus otherObject = (CouponStatus)obj;
        
        return Id == otherObject.Id && Status == otherObject.Status;
    }
    public override int GetHashCode()
    {
        return $"{Id}+{Status}".GetHashCode();
    }
}
