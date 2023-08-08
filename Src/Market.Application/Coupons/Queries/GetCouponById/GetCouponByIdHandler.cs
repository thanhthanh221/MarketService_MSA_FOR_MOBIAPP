using Market.Application.Configurations.Queries;
using Market.Application.Coupons.Queries.AggregateDto;
using Market.Domain.Coupons;

namespace Market.Application.Coupons.Queries.GetCouponById;
public class GetCouponByIdHandler : IQueryHandler<GetCouponByIdQuery, CouponAggregateDto>
{
    private readonly ICouponRepository couponRepository;

    public GetCouponByIdHandler(ICouponRepository couponRepository)
    {
        this.couponRepository = couponRepository;
    }

    public async Task<CouponAggregateDto> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
    {
        var coupon = await couponRepository.GetCouponByIdAsync(request.CouponId);

        return CouponAggregateDto.ConverCouponAggregateToDto(coupon);
    }
}
