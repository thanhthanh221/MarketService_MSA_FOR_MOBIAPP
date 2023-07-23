using Market.Domain.Users;

namespace Market.Domain.Products;

public class ProductUser
{
    public List<UserId> UserFavouriteProduct { get; set; }
}
