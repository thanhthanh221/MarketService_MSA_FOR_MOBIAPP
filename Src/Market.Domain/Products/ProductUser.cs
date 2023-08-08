namespace Market.Domain.Products;

public class ProductUser
{
    public List<Guid> UserFavouriteProduct { get; set; } 
    public int CountEvaluated { get; set; }

    public ProductUser()
    {
        UserFavouriteProduct = new();
        CountEvaluated = 0;
    }
}
