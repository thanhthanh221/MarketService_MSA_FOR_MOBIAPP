using Market.Application.Contracts;

namespace Market.Application.Products.Commands.BuyProduct;
public class BuyProductCommand : CommandBase<bool>
{
    public Guid UserId { get; private set; }
    public List<ProductOrderDataToCommand> ProductOrderDataToCommands { get; private set; }

    public BuyProductCommand(Guid userId, List<ProductOrderDataToCommand> productOrderDataToCommands)
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
