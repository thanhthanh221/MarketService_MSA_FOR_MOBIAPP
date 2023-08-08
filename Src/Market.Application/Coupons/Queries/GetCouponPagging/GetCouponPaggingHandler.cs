using Market.Application.Configurations.Queries;
using Market.Application.Coupons.Queries.AggregateDto;
using Market.Domain.Coupons;

namespace Market.Application.Coupons.Queries.GetCouponPagging;

public class GetCouponPaggingHandler : IQueryHandler<GetCouponPaggingQuery, List<CouponsAggregateDto>>
{
    private readonly ICouponRepository couponRepository;

    public GetCouponPaggingHandler(ICouponRepository couponRepository)
    {
        this.couponRepository = couponRepository;
    }

    public async Task<List<CouponsAggregateDto>> Handle(GetCouponPaggingQuery request, CancellationToken cancellationToken)
    {
        int Page = request.Page;
        int PageSize = request.PageSize;

        if (request.Page < 0) Page = 0;
        if (request.PageSize < 5) PageSize = 7;

        var allCoupon = await couponRepository.GetAllCouponAsync();

        var couponReturn = allCoupon.Where(c => c.CouponStatus.Status != CouponStatus.Deleted.Status)
                            .Skip((Page - 1) * PageSize).Take(PageSize);

        return couponReturn.Select(c => CouponsAggregateDto.ConvertCouponToDto(c)).ToList();
    }
}
