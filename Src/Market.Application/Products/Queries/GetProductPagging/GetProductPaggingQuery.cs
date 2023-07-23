using Market.Application.Contracts;
using Market.Domain.Products;

namespace Market.Application.Products.Queries.GetProductPagging;
public class GetProductPaggingQuery : QueryBase<List<ProductAggregate>>
{
    public GetProductPaggingQuery(int pageSize, int page)
    {
        (PageSize, Page) = (pageSize, page);
    }

    public int PageSize { get; private set; }
    public int Page { get; private set; }

}
