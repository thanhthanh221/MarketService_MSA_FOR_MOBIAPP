namespace Market.Domain.Products.Exceptions;

public class CategoryCreateProductDonotValidate : Exception
{
    public CategoryCreateProductDonotValidate() : base("Category Do not Invalidate")
    {
    }
}
