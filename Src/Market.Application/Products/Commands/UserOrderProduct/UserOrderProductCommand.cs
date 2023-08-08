using Market.Application.Contracts;

namespace Market.Application.Products.Commands.UserOrderProduct;
public class UserOrderProductCommand : CommandBase<bool>
{
    public Guid UserId { get; private set; }
    public List<ProductOrderDataToCommand> ProductOrderDataToCommands { get; private set; }

    public UserOrderProductCommand(Guid userId, List<ProductOrderDataToCommand> productOrderDataToCommands)
    {
        UserId = userId;
        ProductOrderDataToCommands = productOrderDataToCommands;
    }
}

public class ProductOrderDataToCommand
{
    public Guid ProductId { get; set; }
    public Guid ProductTypeValueId { get; set; }
    public int CountOrder { get; set; }
}
