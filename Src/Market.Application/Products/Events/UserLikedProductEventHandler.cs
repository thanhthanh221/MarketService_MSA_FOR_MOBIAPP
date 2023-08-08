using Market.Application.Common.Cache;
using Market.Application.Configurations.Events;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using Newtonsoft.Json;

namespace Market.Application.Products.Events;
public class UserLikedProductEventHandler : IEventHandler<ProductUserFavouriteDomainEvent>
{
    private readonly IReposeCache reposeCache;
    private readonly IProductEventStore productEventStore;

    public UserLikedProductEventHandler(IReposeCache reposeCache,IProductEventStore productEventStore)
    {
        this.reposeCache = reposeCache;
        this.productEventStore = productEventStore;
    }

    public async Task Handle(ProductUserFavouriteDomainEvent @event, CancellationToken cancellationToken)
    {
        await productEventStore.SaveProductDomainEventAsync(@event.ProductId, @event, cancellationToken);

        var cacheKey = CachePatternData.ProductPattern + @event.ProductId.Id;
        var productInCacheToString = await reposeCache.GetCacheReponseAsync(cacheKey);

        if (!string.IsNullOrWhiteSpace(productInCacheToString))
        {
            var productInCache = JsonConvert.DeserializeObject<ProductSnapShot>(productInCacheToString);

            if (productInCache.UserFavouriteProduct.Contains(@event.ProductId.Id))
                productInCache.UserFavouriteProduct.Remove(@event.ProductId.Id);
            else productInCache.UserFavouriteProduct.Add(@event.ProductId.Id);

            await reposeCache.UpdateDataCacheAsync(cacheKey, productInCache);
        }
    }
}
