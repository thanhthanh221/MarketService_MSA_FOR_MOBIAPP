namespace Market.Domain.Products;
public class ProductOrder
{
    public int CountOrder { get; private set; }
    public TimeSpan TimeOrder { get; private set; }

    public ProductOrder(int countOrder, TimeSpan timeOrder)
    {
        CountOrder = countOrder;
        TimeOrder = timeOrder;
    }
}