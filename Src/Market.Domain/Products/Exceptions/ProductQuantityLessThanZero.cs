namespace Market.Domain.Products.Exceptions;

public class ProductQuantityLessThanZero : Exception
{
    public ProductQuantityLessThanZero() : base("Product quantity is less than 0")
    {
    }
}
