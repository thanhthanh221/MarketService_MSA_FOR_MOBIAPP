using Market.Application.Common.Cache;
using Market.Application.Configurations.Events;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Market.Application.Products.Events;
public class UserOrderedSuscessProductEventHandler : IEventHandler<ProductUserOrderedProductSuccessDomainEvent>
{
    private readonly IProductEventStore productEventStore;
    private readonly IReposeCache reponseCache;
    private readonly ILogger<UserOrderedSuscessProductEventHandler> logger;

    public UserOrderedSuscessProductEventHandler(IProductEventStore productEventStore,
        IReposeCache reponseCache, ILogger<UserOrderedSuscessProductEventHandler> logger)
    {
        this.productEventStore = productEventStore;
        this.reponseCache = reponseCache;
        this.logger = logger;
    }

    public async Task Handle(ProductUserOrderedProductSuccessDomainEvent @event, CancellationToken cancellationToken)
    {
        await productEventStore.SaveProductDomainEventAsync(@event.ProductId, @event, cancellationToken);
        var cacheKey = CachePatternData.ProductPattern + @event.ProductId.Id;

        var cacheDataString = await reponseCache.GetCacheReponseAsync(cacheKey);

        if (string.IsNullOrWhiteSpace(cacheDataString))
        {
            var productSnapShot = JsonConvert.DeserializeObject<ProductSnapShot>(cacheDataString);

            productSnapShot.ProductTypeValues.ForEach(p =>
            {
                if (@event.ProductTypeUserOrder.ValueTypeOrderId.TypeId == p.ProductTypeValueId.TypeId)
                {
                    int countOrder = @event.ProductTypeUserOrder.CountOrder;
                    p.SetQuantityProductType(p.QuantityType - countOrder);
                    p.SetQuantityProductTypeSold(p.QuantityProductTypeSold + countOrder);
                }
            });

            await reponseCache.UpdateDataCacheAsync(cacheKey, productSnapShot);

            logger.LogInformation($"Update Cache Product {productSnapShot.ProductId} When User Order Suscess Product");
        }
    }
}
