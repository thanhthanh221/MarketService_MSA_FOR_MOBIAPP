using Market.Application.Contracts;
using Market.Application.Coupons.Queries.AggregateDto;

namespace Market.Application.Coupons.Queries.GetCouponPagging;
public class GetCouponPaggingQuery : QueryBase<List<CouponsAggregateDto>>
{
    public int Page { get; private set; }
    public int PageSize { get; private set; }
}
