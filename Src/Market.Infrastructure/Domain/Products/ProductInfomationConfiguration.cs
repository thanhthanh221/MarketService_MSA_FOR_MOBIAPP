using Market.Domain.Products;
using Market.Infrastructure.MongoDb;

namespace Market.Infrastructure.Domain.Products;

public class ProductInfomationConfiguration : MongoDbConfigurationEntity<ProductInfomation>
{
    public override void Configure()
    {
        this.AutoMap();
    }
}
