using Market.Application.Contracts;

namespace Market.Application.Products.Commands.UserEvaluateProduct;
public class UserEvaluateProductCommand : CommandBase<Guid>
{
    public Guid UserId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Star { get; private set; }

    public UserEvaluateProductCommand(Guid userId, Guid productId, int star)
    {
        UserId = userId;
        ProductId = productId;
        Star = star;
    }
}
