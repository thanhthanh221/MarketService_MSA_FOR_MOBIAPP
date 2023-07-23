using Market.Domain.Products;
using Market.Domain.Users;
using Market.Infrastructure.Domain.Products;
using Market.Infrastructure.MarketContext;
using Market.Infrastructure.MongoDb;
using Microsoft.Extensions.Options;

namespace Market.IntegrationTests.Products;
public class ProductIntergrationTestsFixture : IDisposable
{
    private readonly MarketDbContext marketDbContext;
    public readonly ProductRepository productRepository;

    public ProductIntergrationTestsFixture()
    {
        marketDbContext = new(Options.Create(new MongoDbSettings() {
            Host = "Localhost",
            Port = "27017",
            Name = "market_IntegrationsProducts"
        }));
        productRepository = new(marketDbContext.Products);
    }
    public static ProductAggregate CreateProductDataFake()
    {
        ProductId productId = new(Guid.NewGuid());
        ProductInfomation productInfomation = new(Guid.NewGuid().ToString()[..11], 1000, 10, Guid.NewGuid().ToString()[..5], 3, "https://eatcleanhub.com/wp-content/uploads/2022/02/bim1.jpg", DateTime.UtcNow);
        ProductType productType = new(Guid.NewGuid().ToString(),
        new List<ProductTypeValue>(){
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),100,27),
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),20,63),
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),35,58),

        });
        List<ProductCategory> productCategories = new() {
            ProductCategory.Bbq,
            ProductCategory.Bread
        };
        ProductUser productUser = new() {
            UserFavouriteProduct = new List<UserId>() {
                new UserId(Guid.NewGuid()),
                new UserId(Guid.NewGuid())
            }
        };
        ProductOrder productOrder = new(0, new TimeSpan(1, 10, 10));

        ProductAggregate product = new(
            productId,
            productInfomation,
            productType, ProductStatus.Active,
            productCategories,
            productUser,
            productOrder
            );
        
        return product;

    }

    public void Dispose()
    {
        marketDbContext.Client.DropDatabase("market_IntegrationsProducts");
    }
}
