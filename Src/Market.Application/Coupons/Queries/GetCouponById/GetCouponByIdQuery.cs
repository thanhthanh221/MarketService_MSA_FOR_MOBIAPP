using Market.Application.Contracts;
using Market.Application.Coupons.Queries.AggregateDto;
using Market.Domain.Coupons;

namespace Market.Application.Coupons.Queries.GetCouponById;
public class GetCouponByIdQuery : QueryBase<CouponAggregateDto>
{
    public CouponId CouponId { get; private set; }
    public GetCouponByIdQuery(CouponId couponId)
    {
        CouponId = couponId;
    }
}
