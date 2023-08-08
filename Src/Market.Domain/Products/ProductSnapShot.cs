namespace Market.Domain.Products;
public class ProductSnapShot
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Calo { get; set; }
    public string Descretion { get; set; }
    public double Star { get; set; }
    public int CountEvaluated { get; set; }
    public string ProductImageUri { get; set; }
    public DateTime CreateAt { get; set; }
    public string ProductStatus { get; set; }
    public int CountOrder { get; set; }
    public TimeSpan TimeOrder { get; set; }
    public List<ProductCategory> Categories { get; set; }
    public List<Guid> UserFavouriteProduct { get; set; }

    public string ProductTypeName { get; set; }
    public List<ProductTypeValue> ProductTypeValues { get; set; }

    public static ProductSnapShot ConvertProductToShapshot(ProductAggregate product)
    {
        return new()
        {
            ProductId = product.ProductId.Id,
            Name = product.ProductInfomation.Name,
            Calo = product.ProductInfomation.Calo,
            Price = product.ProductInfomation.Price,
            Star = product.ProductInfomation.Star,
            CountEvaluated = product.ProductUser.CountEvaluated,
            ProductImageUri = product.ProductInfomation.ProductImageUri,
            CreateAt = product.ProductInfomation.CreateAt,
            CountOrder = product.ProductOrder.CountOrder,
            TimeOrder = product.ProductOrder.TimeOrder,
            Categories = product.Categories,
            UserFavouriteProduct = product.ProductUser.UserFavouriteProduct,
            ProductTypeName = product.ProductType.ProductTypeName,
            ProductTypeValues = product.ProductType.ProductTypeValues
        };
    }

}
