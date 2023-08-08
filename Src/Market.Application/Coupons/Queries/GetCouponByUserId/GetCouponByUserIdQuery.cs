using Market.Application.Contracts;
using Market.Application.Coupons.Queries.AggregateDto;
using Market.Domain.Users;

namespace Market.Application.Coupons.Queries.GetCouponByUserId;
public class GetCouponByUserIdQuery : QueryBase<List<CouponsAggregateDto>>
{
    public UserId UserId { get; private set; }
    public int Page { get; private set; }
    public int PageSize { get; private set; }

    public GetCouponByUserIdQuery(UserId userId, int page, int pageSize)
    {
        UserId = userId;
        Page = page;
        PageSize = pageSize;
    }
}
