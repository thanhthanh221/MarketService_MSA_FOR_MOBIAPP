namespace Market.Domain.Products.Exceptions;
public class ProductHasBeenDeleted : Exception
{
    public ProductHasBeenDeleted() : base("he product has been deleted")
    {
    }
}
