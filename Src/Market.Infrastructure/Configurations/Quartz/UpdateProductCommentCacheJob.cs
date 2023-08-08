using Market.Application.Common.Cache;
using Market.Domain.ProductComments;
using Microsoft.Extensions.Logging;
using Quartz;
namespace Market.Infrastructure.Configurations.Quartz;

public class UpdateProductCommentCacheJob : IJob
{
    private readonly IProductCommentRepository productCommentRepository;
    private readonly ILogger<UpdateProductCommentCacheJob> logger;
    private readonly IReposeCache reposeCache;

    public UpdateProductCommentCacheJob(IProductCommentRepository productCommentRepository,
        ILogger<UpdateProductCommentCacheJob> logger, IReposeCache reposeCache)
    {
        this.productCommentRepository = productCommentRepository;
        this.logger = logger;
        this.reposeCache = reposeCache;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var allProductComment = await productCommentRepository.GetAllCommentAsync();

        await reposeCache.RemoveCacheByPatternAsync(CachePatternData.ProductCommentPattern);

        allProductComment.ForEach(async p =>
        {
            await reposeCache.SetCacheReponseAsync(CachePatternData.ProductCommentPattern + p.ProductCommentId.Id,
                ProductCommentSnapshot.ConvertAggregateToSnapshot(p),
                new TimeSpan(24, 0, 0));
        });
        logger.LogInformation("Update Product Comment Data In Cache");
    }
}
