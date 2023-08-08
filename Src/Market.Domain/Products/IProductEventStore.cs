namespace Market.Domain.Products;
public interface IProductEventStore
{
    Task SaveProductDomainEventAsync(ProductAggregate productAggregate);
    Task SaveProductDomainEventAsync(ProductId productId, object domainEvent, CancellationToken cancellationToken = default);
    Task GetProductDomainEventAsync(ProductId productId);
}
