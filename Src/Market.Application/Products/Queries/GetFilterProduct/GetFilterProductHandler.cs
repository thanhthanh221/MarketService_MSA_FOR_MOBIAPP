using Market.Application.Common.Cache;
using Market.Application.Configurations.Queries;
using Market.Application.Products.Queries.AggregateDto;
using Market.Domain.Products;
using Newtonsoft.Json;

namespace Market.Application.Products.Queries.GetFilterProduct;
public class GetFilterProductHandler : IQueryHandler<GetFilterProductQuery, List<ProductsAggregateDto>>
{
    private readonly IReposeCache reposeCache;
    private readonly IProductRepository productRepository;

    public GetFilterProductHandler(
        IReposeCache reposeCache, IProductRepository productRepository)
    {
        this.reposeCache = reposeCache;
        this.productRepository = productRepository;
    }

    public async Task<List<ProductsAggregateDto>> Handle(GetFilterProductQuery request, CancellationToken cancellationToken)
    {
        List<ProductAggregate> productReturn = new();
        var productInCache =
            await reposeCache.GetCacheReponseByPatternAsync(CachePatternData.ProductCommentPattern);

        if (productInCache.Count != 0) {
            productInCache.ForEach(p => {
                productReturn.Add(JsonConvert.DeserializeObject<ProductAggregate>(p));
            });
        }

        return null;
    }
}
