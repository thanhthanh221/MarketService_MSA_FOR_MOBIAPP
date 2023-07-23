using Market.Application.Contracts;
using Market.Domain.Products;
using Market.Domain.Users;

namespace Market.Application.Products.Commands.OrderProduct;
public class OrderProductCommand : CommandBase<Guid>
{
    public ProductId ProductId { get; private set; }
    public UserId UserId { get; private set; }
    public List<ProductOrderItemCommand> ProductOrderItemCommands { get; private set; }

    public OrderProductCommand(ProductId productId, List<ProductOrderItemCommand> productOrderItemCommands)
    {
        ProductId = productId;
        ProductOrderItemCommands = productOrderItemCommands;
    }
}

public record ProductOrderItemCommand(ProductTypeValueId ProductTypeValueId, int CountOrder);
