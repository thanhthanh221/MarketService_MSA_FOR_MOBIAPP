using Market.Domain.Coupons;
using Market.Domain.ProductComments;
using Market.Domain.Products;
using Market.Domain.Users;
using Market.Infrastructure.Domain.Coupons;
using Market.Infrastructure.Domain.ProductComments;
using Market.Infrastructure.Domain.Products;
using Market.Infrastructure.Domain.Users;
using Market.Infrastructure.MarketContext;
using Market.Infrastructure.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Market.Infrastructure.Dependency;
public class MongoDbDependency : IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetRequiredSection(nameof(MongoDbSettings)));

        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); // Chỉnh Giud thành String
        BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String)); // Ngày tháng thành String

        // Config Entity Props Entity
        BsonClassMap.RegisterClassMap<ProductAggregate>(p => ProductConfiguration.Configure(p));
        BsonClassMap.RegisterClassMap<ProductCommentAggregate>(p => ProductCommentConfiguration.Configure(p));
        BsonClassMap.RegisterClassMap<CouponAggregate>(c => CouponConfiguration.Configure(c));
        BsonClassMap.RegisterClassMap<UserAggregate>(u => UserConfiguration.Configure(u));

        services.AddTransient<MarketDbContext>();
    }
}
