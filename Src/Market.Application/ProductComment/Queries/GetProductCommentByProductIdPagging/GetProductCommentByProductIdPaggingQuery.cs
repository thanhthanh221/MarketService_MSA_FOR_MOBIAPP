using Market.Application.Contracts;
using Market.Application.ProductComment.Queries.AggregateDto;
using Market.Domain.ProductComments;
using Market.Domain.Products;

namespace Market.Application.ProductComment.Queries.GetProductCommentByProductIdPagging;
public class GetProductCommentByProductIdPaggingQuery : QueryBase<List<ProductCommentsDto>>
{
    public ProductId ProductId { get; private set; }
    public int Page { get; private set; }
    public int PageSize { get; private set; }

    public GetProductCommentByProductIdPaggingQuery(ProductId productId, int page, int pageSize)
    {
        ProductId = productId;
        Page = page;
        PageSize = pageSize;
    }
}
