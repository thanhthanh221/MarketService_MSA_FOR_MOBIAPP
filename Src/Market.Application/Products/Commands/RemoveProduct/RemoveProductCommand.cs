using Market.Application.Contracts;
using Market.Domain.Products;

namespace Market.Application.Products.Commands.RemoveProduct;

public class RemoveProductCommand : CommandBase<Guid>
{
    public ProductId ProductId { get; set; }

    public RemoveProductCommand(ProductId productId)
    {
        ProductId = productId;
    }
}
