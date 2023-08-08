using Market.Application.Common.Cache;
using Market.Application.Configurations.Events;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using Newtonsoft.Json;

namespace Market.Application.Products.Events;
public class UserRemovedProductEventHandler : IEventHandler<ProductRemovedDomainEvent>
{
    private readonly IReposeCache reposeCache;
    private readonly IProductEventStore productEventStore;
    public UserRemovedProductEventHandler(IReposeCache reposeCache,IProductEventStore productEventStore)
    {
        this.reposeCache = reposeCache;
        this.productEventStore = productEventStore;
    }
    public async Task Handle(ProductRemovedDomainEvent @event, CancellationToken cancellationToken)
    {
        await productEventStore.SaveProductDomainEventAsync(@event.ProductId, @event, cancellationToken);

        var cacheKey = CachePatternData.ProductPattern + @event.ProductId.Id;
        var productInCacheToString = await reposeCache.GetCacheReponseAsync(cacheKey);

        if (!string.IsNullOrWhiteSpace(productInCacheToString))
        {
            var productInCache = JsonConvert.DeserializeObject<ProductSnapShot>(productInCacheToString);

            productInCache.ProductStatus = ProductStatus.Remove.StatusValue;

            await reposeCache.UpdateDataCacheAsync(cacheKey, productInCache);
        }
    }
}
