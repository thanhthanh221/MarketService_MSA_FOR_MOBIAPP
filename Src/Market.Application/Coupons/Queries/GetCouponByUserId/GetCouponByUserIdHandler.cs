using Market.Application.Common.Cache;
using Market.Application.Configurations.Queries;
using Market.Application.Coupons.Queries.AggregateDto;
using Market.Domain.Coupons;

namespace Market.Application.Coupons.Queries.GetCouponByUserId;
public class GetCouponByUserIdHandler : IQueryHandler<GetCouponByUserIdQuery, List<CouponsAggregateDto>>
{
    private readonly ICouponRepository couponRepository;
    private readonly IReposeCache reposeCache;
    public GetCouponByUserIdHandler(
        ICouponRepository couponRepository, IReposeCache reposeCache)
    {
        this.couponRepository = couponRepository;
        this.reposeCache = reposeCache;
    }

    public async Task<List<CouponsAggregateDto>> Handle(GetCouponByUserIdQuery request, CancellationToken cancellationToken)
    {
        var couponInDatabase = await couponRepository.GetAllCouponAsync();

        var couponInDatabaseByUserId = from c in couponInDatabase
                                       where c.CouponUsers.Any(u => u.UserId.Equals(request.UserId))
                                       select c;

        int Page = request.Page;
        int PageSize = request.PageSize;

        if (request.Page < 0) Page = 0;
        if (request.PageSize < 5) PageSize = 7;

        var couponInDatabaseByUserIdPagging = couponInDatabaseByUserId
            .Skip((Page - 1) * PageSize).Take(PageSize);

        return couponInDatabaseByUserIdPagging
            .Select(c => CouponsAggregateDto.ConvertCouponToDto(c)).ToList();

    }
}
