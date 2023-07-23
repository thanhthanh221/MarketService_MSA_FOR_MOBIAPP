

namespace Market.Domain.Products.Exceptions;

public class UserDonotFavouriteProduct : Exception
{
    public UserDonotFavouriteProduct() : base("User Has Not Favourited Product")
    {
    }
}
