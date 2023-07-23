namespace Market.Domain.Products.Exceptions;
public class ProductTypeInStocksIsNotEnough : Exception
{
    public ProductTypeInStocksIsNotEnough() : base("The number of products type in stock is not enough")
    {
    }
}
