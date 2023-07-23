namespace Market.Application.Products.Queries.AggregateDto;
public class ProductsAggregateDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public bool RequestByUserCheckFavouriteProduct { get; set; } = false;
    public double Star { get; set; }
    public decimal Price { get; private set; }
    public string ProductImageUri { get; set; }
}
