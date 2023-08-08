using Market.Application.Common.Cache;
using Market.Application.Configurations.Queries;
using Market.Application.Products.Queries.AggregateDto;
using Market.Domain.Products;
using Newtonsoft.Json;

namespace Market.Application.Products.Queries.GetProductPagging;

public class GetProductPaggingHandler : IQueryHandler<GetProductPaggingQuery, List<ProductsAggregateDto>>
{
    private readonly IProductRepository productRepository;
    private readonly IReposeCache reponseCache;

    public GetProductPaggingHandler(IProductRepository productRepository, IReposeCache reponseCache)
    {
        this.productRepository = productRepository;
        this.reponseCache = reponseCache;
    }

    public async Task<List<ProductsAggregateDto>> Handle(GetProductPaggingQuery request, CancellationToken cancellationToken)
    {
        int Page = request.Page;
        int PageSize = request.PageSize;

        if (request.Page < 0) Page = 0;
        if (request.PageSize < 5) PageSize = 5;

        var productInCacheData = await reponseCache.GetCacheReponseByPatternAsync(CachePatternData.ProductPattern);
        if (productInCacheData.Count != 0)
        {
            List<string> productInCacheDataPagging = productInCacheData
                .Skip((Page - 1) * PageSize).Take(PageSize).ToList();

            var productInCachePagging = productInCacheDataPagging.Select(p =>
            {
                return JsonConvert.DeserializeObject<ProductSnapShot>(p);
            });
            var productsDtoReturn = productInCachePagging.Select(p => 
                ProductsAggregateDto.ConverProductSnapShotToDtoByUser(p, request.UserId));
            return productsDtoReturn.ToList();
        }

        var paggingProduct = await productRepository.GetProductPagingAsync(Page, PageSize);

        var productPaggingToDto = paggingProduct.Select(p => 
            ProductsAggregateDto.ConvertProductToDtoByUser(p,request.UserId));
        return productPaggingToDto.ToList();
    }
}
