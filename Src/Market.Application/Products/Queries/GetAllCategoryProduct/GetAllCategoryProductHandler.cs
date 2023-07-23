using Market.Application.Configurations.Queries;
using Market.Domain.Products;

namespace Market.Application.Products.Queries.GetAllCategoryProduct;

public class GetAllCategoryProductHandler : IQueryHandler<GetAllCategoryProductQuery, List<ProductCategory>>
{
    public Task<List<ProductCategory>> Handle(GetAllCategoryProductQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(ProductCategory.GetAllCategoryProduct().ToList());
    }
}
