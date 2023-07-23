using Market.Domain.Products;
using Market.Domain.Users;

namespace Market.Application.Products.Queries.AggregateDto;
public class ProductAggregateDto
{
    public Guid ProductId { get; set; }
    public bool RequestByUserCheckFavouriteProduct { get; set; } = false;
    public string ProductName { get; set; }
    public int Calo { get; set; }
    public string Descretion { get; set; }
    public double Star { get; set; }
    public string ProductImageUri { get; set; }
    public string ProductTypeName { get; set; }
    public List<ProductTypeValue> ProductTypeValues { get; set; }
    public ProductStatus ProductStatus { get; set; }
    public TimeSpan TimeOrder { get; set; }

    public static ProductAggregateDto ConvertProductAggregateToProductDto(ProductAggregate productAggregate)
    {
        return new ProductAggregateDto() {
            ProductId = productAggregate.ProductId.Id,
            ProductName = productAggregate.ProductInfomation.Name,
            Calo = productAggregate.ProductInfomation.Calo,
            Descretion = productAggregate.ProductInfomation.Descretion,
            Star = productAggregate.ProductInfomation.Star,
            ProductImageUri = productAggregate.ProductInfomation.ProductImageUri,
            ProductTypeName = productAggregate.ProductType.ProductTypeName,
            ProductTypeValues = productAggregate.ProductType.ProductTypeValues,
            ProductStatus = productAggregate.ProductStatus,
            TimeOrder = productAggregate.ProductOrder.TimeOrder
        };

    }
    public static ProductAggregateDto ConvertProductAggregateToProductDtoByUserRequest(ProductAggregate productAggregate, UserId userId)
    {
        return new ProductAggregateDto() {
            ProductId = productAggregate.ProductId.Id,
            RequestByUserCheckFavouriteProduct = productAggregate.ProductUser.UserFavouriteProduct.Any(c =>c == userId),
            ProductName = productAggregate.ProductInfomation.Name,
            Calo = productAggregate.ProductInfomation.Calo,
            Descretion = productAggregate.ProductInfomation.Descretion,
            Star = productAggregate.ProductInfomation.Star,
            ProductImageUri = productAggregate.ProductInfomation.ProductImageUri,
            ProductTypeName = productAggregate.ProductType.ProductTypeName,
            ProductTypeValues = productAggregate.ProductType.ProductTypeValues,
            ProductStatus = productAggregate.ProductStatus,
            TimeOrder = productAggregate.ProductOrder.TimeOrder
        };

    }

}
