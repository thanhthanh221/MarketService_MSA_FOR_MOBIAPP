using Market.Application.Common.Cache;
using Market.Application.Configurations.Events;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using Microsoft.Extensions.Logging;

namespace Market.Application.Products.Events;
public class UserOrderedSuscessProductEventHandler : IEventHandler<ProductUserOrderedProductSuccessDomainEvent>
{
    private readonly IProductEventStore productEventStore;
    private readonly IProductRepository productRepository;
    private readonly IReposeCache reponseCache;
    private readonly ILogger<UserOrderedSuscessProductEventHandler> logger;

    public UserOrderedSuscessProductEventHandler(
        IProductEventStore productEventStore, IProductRepository productRepository,
        IReposeCache reponseCache, ILogger<UserOrderedSuscessProductEventHandler> logger)
    {
        this.productEventStore = productEventStore;
        this.productRepository = productRepository;
        this.reponseCache = reponseCache;
        this.logger = logger;
    }

    public async Task Handle(ProductUserOrderedProductSuccessDomainEvent @event, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(@event.ProductId);

        await productEventStore.SaveProductDomainEventAsync(product);
        
        // Save Data To Cache(Create To Read Data Base)
        await reponseCache.UpdateDataCacheAsync(
            CachePatternData.ProductPattern + product.ProductId.Id, product);

        logger.LogInformation($"Update Cache Product {product.ProductId.Id} When User Order Suscess Product");
    }
}
