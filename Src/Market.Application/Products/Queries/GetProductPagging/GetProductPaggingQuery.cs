using Market.Application.Contracts;
using Market.Application.Products.Queries.AggregateDto;
using Market.Domain.Users;

namespace Market.Application.Products.Queries.GetProductPagging;
public class GetProductPaggingQuery : QueryBase<List<ProductsAggregateDto>>
{
    public UserId UserId {get; private set;}
    public int PageSize { get; private set; }
    public int Page { get; private set; }

    public GetProductPaggingQuery(UserId userId, int pageSize, int page)
    {
        UserId = userId;
        PageSize = pageSize;
        Page = page;
    }
    public GetProductPaggingQuery(int pageSize, int page)
    {
        PageSize = pageSize;
        Page = page;
    }

}
