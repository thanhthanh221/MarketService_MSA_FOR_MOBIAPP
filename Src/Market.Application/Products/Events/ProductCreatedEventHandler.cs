using Market.Application.Common.Cache;
using Market.Application.Configurations.Events;
using Market.Domain.Products;
using Market.Domain.Products.Events;

namespace Market.Application.Products.Events;

public class ProductCreatedEventHandler : IEventHandler<ProductCreatedDomainEvent>
{
    private readonly IProductEventStore productEventStore;
    private readonly IReposeCache reponseCache;

    public ProductCreatedEventHandler(IProductEventStore productEventStore, IReposeCache reponseCache)
    {
        this.productEventStore = productEventStore;
        this.reponseCache = reponseCache;
    }

    public async Task Handle(ProductCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        // Save Event To Event Store
        await productEventStore.SaveProductDomainEventAsync(@event.ProductAggregate.ProductId, 
            @event, cancellationToken);

        var productSnapShot = ProductSnapShot.ConvertProductToShapshot(@event.ProductAggregate);
        // Save Data To Cache(Create To Read Data Base)
        await reponseCache.SetCacheReponseAsync(
            CachePatternData.ProductPattern + @event.ProductAggregate.ProductId.Id,
            productSnapShot,
            new TimeSpan(24, 0, 0));
    }
}
