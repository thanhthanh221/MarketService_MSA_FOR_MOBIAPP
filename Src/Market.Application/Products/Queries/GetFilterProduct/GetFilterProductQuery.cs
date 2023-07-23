using Market.Application.Contracts;
using Market.Application.Products.Queries.AggregateDto;
using Market.Domain.Products;

namespace Market.Application.Products.Queries.GetFilterProduct;
public class GetFilterProductQuery : QueryBase<List<ProductsAggregateDto>>
{
    public decimal MinPrice { get; private set; }
    public decimal MaxPrice { get; private set; }

    public int MinStar { get; private set; }
    public int MaxStar { get; private set; }

    public List<ProductCategory> Categories { get; private set; }

    public GetFilterProductQuery(
        decimal minPrice,
        decimal maxPrice,
        int minStar,
        int maxStar,
        List<ProductCategory> categories)
    {
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        MinStar = minStar;
        MaxStar = maxStar;
        Categories = categories;
    }

}
