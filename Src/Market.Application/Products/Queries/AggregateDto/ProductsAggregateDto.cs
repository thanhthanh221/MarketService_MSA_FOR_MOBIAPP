using Market.Domain.Products;
using Market.Domain.Users;

namespace Market.Application.Products.Queries.AggregateDto;
public class ProductsAggregateDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public bool RequestByUserCheckFavouriteProduct { get; set; } = false;
    public int CountUserFavourite { get; set; }
    public int CountEvaluate { get; set; }
    public double Star { get; set; }
    public decimal Price { get; private set; }
    public int Calo { get; set; }
    public string ProductImageUri { get; set; }
    
    public static ProductsAggregateDto ConvertProductToDtoByUser(ProductAggregate product, UserId userId)
    {
        return new()
        {
            ProductId = product.ProductId.Id,
            RequestByUserCheckFavouriteProduct = 
                product.ProductUser.UserFavouriteProduct.Any(u => u.Equals(userId.Id)),
            ProductName = product.ProductInfomation.Name,
            CountUserFavourite = product.ProductUser.UserFavouriteProduct.Count,
            CountEvaluate = product.ProductUser.CountEvaluated,
            Star = product.ProductInfomation.Star,
            Price = product.ProductInfomation.Price,
            Calo = product.ProductInfomation.Calo,
            ProductImageUri = product.ProductInfomation.ProductImageUri
        };
    }
    public static ProductsAggregateDto ConverProductSnapShotToDtoByUser(ProductSnapShot productSnapShot, UserId userId)
    {
        return new()
        {
            ProductId = productSnapShot.ProductId,
            RequestByUserCheckFavouriteProduct = 
                productSnapShot.UserFavouriteProduct.Any(u => u.Equals(userId.Id)),
            ProductName = productSnapShot.Name,
            CountUserFavourite = productSnapShot.UserFavouriteProduct.Count,
            CountEvaluate = productSnapShot.CountEvaluated,
            Star = productSnapShot.Star,
            Price = productSnapShot.Price,
            Calo = productSnapShot.Calo,
            ProductImageUri = productSnapShot.ProductImageUri
        };
    }   
}
