

namespace Market.Domain.Products.Exceptions;

public class UserFavouriteProduct : Exception
{
    public UserFavouriteProduct() : base("User Has Favourited Product")
    {
    }
}
