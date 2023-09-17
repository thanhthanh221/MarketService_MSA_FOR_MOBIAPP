using Market.Application.Contracts;
using Market.Domain.Products;
using Market.Domain.Users;

namespace Market.Application.Products.Commands.FavouriteProduct;
public class FavouriteProductCommand : CommandBase<bool>
{
    public ProductId ProductId { get; private set; }
    public UserId User { get; private set; }

    public FavouriteProductCommand(ProductId productId, UserId user)
    {
        ProductId = productId;
        User = user;
    }
}
