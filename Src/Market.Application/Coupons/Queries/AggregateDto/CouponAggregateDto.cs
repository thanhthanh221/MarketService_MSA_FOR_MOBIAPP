using Market.Domain.Coupons;

namespace Market.Application.Coupons.Queries.AggregateDto;
public class CouponAggregateDto
{
    public Guid CouponId { get; set; }
    public string Title { get; set; }
    public int Amount { get; set; }
    public string CouponStatus { get; set; }
    public DateTime Expired { get; set; }

    public static CouponAggregateDto ConverCouponAggregateToDto(CouponAggregate coupon)
    {
        return new() {
            CouponId = coupon.CouponId.Id,
            Title = coupon.CouponInfomation.Titile,
            Amount = coupon.CouponInfomation.Amount,
            CouponStatus = coupon.CouponStatus.Status,
            Expired = coupon.CouponInfomation.Expired
        };
    }
}
