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
        var productDataInCache = await reponseCache.GetCacheReponseByPatternAsync(CachePatternData.ProductPattern);

        if(productDataInCache.Count != 0) {
            var productInCacheData = await reponseCache.GetCacheReponseAsync($"{CachePatternData.ProductPattern}{request.ProductId}");
            var productInCache = JsonConvert.DeserializeObject<ProductAggregate>(productInCacheData);
            return ProductAggregateDto.ConvertProductAggregateToProductDtoByUserRequest(productInCache, request.UserId);
        } 
        var productInDataBase = await productRepository.GetProductByIdAsync(request.ProductId);

        return ProductAggregateDto.ConvertProductAggregateToProductDtoByUserRequest(productInDataBase, request.UserId);
    }
}
