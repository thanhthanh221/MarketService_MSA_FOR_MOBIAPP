using Market.Application.Common.Cache;
using Market.Application.Configurations.Queries;
using Market.Application.Products.Queries.AggregateDto;
using Market.Domain.Products;
using Newtonsoft.Json;

namespace Market.Application.Products.Queries.GetProductById;
public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, ProductAggregateDto>
{
    private readonly IProductRepository productRepository;
    private readonly IReposeCache reponseCache;

    public GetProductByIdHandler(IProductRepository productRepository, IReposeCache reponseCache)
    {
        this.productRepository = productRepository;
        this.reponseCache = reponseCache;
    }

    public async Task<ProductAggregateDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = CachePatternData.ProductPattern + request.ProductId;
        var productInCacheData = await reponseCache.GetCacheReponseAsync(cacheKey);

        if (!string.IsNullOrEmpty(productInCacheData))
        {
            var productInCache = JsonConvert.DeserializeObject<ProductSnapShot>(productInCacheData);
            return ProductAggregateDto.ConvertProductSnapshotToDtoByUser(productInCache, request.UserId);
        }
        var productInDataBase = await productRepository.GetProductByIdAsync(request.ProductId);

        return ProductAggregateDto.ConvertProductAggregateToProductDtoByUserRequest(productInDataBase, request.UserId);
    }
}
