using Market.Domain.Coupons;

namespace Market.Application.Coupons.Queries.AggregateDto;
public class CouponsAggregateDto
{
    public Guid CouponId { get; set; }
    public string Code { get; set; }
    public List<string> Descriptios { get; set; }
    public DateTime Expired { get; set; }
    public string CouponStatus { get; set; }

    public static CouponsAggregateDto ConvertCouponToDto(CouponAggregate coupon){
        return new() {
            CouponId = coupon.CouponId.Id,
            Code = coupon.CouponInfomation.Code,
            Descriptios = coupon.CouponInfomation.Descriptios,
            Expired = coupon.CouponInfomation.Expired,
            CouponStatus = coupon.CouponStatus.Status
        };
    }
}
