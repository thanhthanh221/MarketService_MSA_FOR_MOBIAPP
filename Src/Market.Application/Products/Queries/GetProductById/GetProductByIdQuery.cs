using Market.Application.Contracts;
using Market.Application.Products.Queries.AggregateDto;
using Market.Domain.Products;
using Market.Domain.Users;

namespace Market.Application.Products.Queries.GetProductById;
public class GetProductByIdQuery : QueryBase<ProductAggregateDto>
{
    public ProductId ProductId { get; private set; }
    public UserId UserId { get; private set; }

    public GetProductByIdQuery(ProductId productId, UserId userId)
    {
        ProductId = productId;
        UserId = userId;
    }

    public GetProductByIdQuery(ProductId productId)
    {
        ProductId = productId;
    }
}
