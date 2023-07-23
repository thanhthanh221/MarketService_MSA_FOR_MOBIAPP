namespace Market.Domain.Products.Exceptions;
public class AllAddedProductTypesAlreadyExist : Exception
{
    public AllAddedProductTypesAlreadyExist() : base("All added product types already exist")
    {
    }
}
