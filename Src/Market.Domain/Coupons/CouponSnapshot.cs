namespace Market.Domain.Coupons;
public class CouponSnapshot
{
    public Guid CouponId { get; set; }
    public string Code { get; set; }
    public string Titile { get; set; }
    public List<string> Descriptios { get; set; }
    public decimal PriceMinOrder { get; set; }
    public decimal PriceReduced { get; set; }
    public int Amount { get; set; }
    public int CountCouponUse { get; set; }
    public Guid AdminId { get; set; }
    public DateTime Expired { get; set; }
    public List<CouponUser> CouponUsers { get; set; }
    public string CouponStatus { get; set; }
    public long Version { get; set; }

    public static CouponSnapshot ConverConponToSnapshot(CouponAggregate coupon)
    {
        return new() {
            CouponId = coupon.CouponId.Id,
            Code = coupon.CouponInfomation.Code,
            Titile = coupon.CouponInfomation.Titile,
            Descriptios = coupon.CouponInfomation.Descriptios,
            PriceMinOrder = coupon.CouponInfomation.PriceMinOrder,
            PriceReduced =  coupon.CouponInfomation.PriceMinOrder,
            Amount = coupon.CouponInfomation.Amount,
            CountCouponUse = coupon.CouponInfomation.CountCouponUse,
            AdminId = coupon.CouponInfomation.AdminId.Id,
            Expired = coupon.CouponInfomation.Expired,
            CouponUsers = coupon.CouponUsers.ToList(),
            CouponStatus = coupon.CouponStatus.Status,
            Version = coupon.Version
        };
    }
}
