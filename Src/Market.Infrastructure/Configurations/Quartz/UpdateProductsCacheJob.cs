using Market.Application.Common.Cache;
using Market.Domain.Products;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Market.Infrastructure.Configurations.Quartz;
public class UpdateProductsCacheJob : IJob
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<UpdateProductsCacheJob> logger;
    private readonly IReposeCache reposeCache;

    public UpdateProductsCacheJob(
        IProductRepository productRepository, ILogger<UpdateProductsCacheJob> logger, IReposeCache reposeCache)
    {
        this.productRepository = productRepository;
        this.logger = logger;
        this.reposeCache = reposeCache;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var allProduct = await productRepository.GetAllProductAsync();

        await reposeCache.RemoveCacheByPatternAsync(CachePatternData.ProductPattern);

        allProduct.ForEach(async p =>
        {
            await reposeCache.SetCacheReponseAsync(CachePatternData.ProductPattern + p.ProductId.Id,
                ProductSnapShot.ConvertProductToShapshot(p),
                new TimeSpan(24, 0, 0));
        });
        logger.LogInformation("Update Product Data In Cache");
    }
}
