namespace Market.Domain.Products;
public interface IProductEventStore
{
    Task SaveProductDomainEventAsync(ProductAggregate productAggregate);
    Task GetProductDomainEventAsync(ProductId productId);
}
