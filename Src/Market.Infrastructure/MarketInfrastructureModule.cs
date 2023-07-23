using Autofac;
using Market.Infrastructure.Domain.Products;
using Market.Infrastructure.MarketContext;
using Market.Infrastructure.MongoDb;
using Microsoft.Extensions.Configuration;

namespace Market.Infrastructure;

public class MarketInfrastructureModule : Module
{
    private readonly IConfiguration configuration;

    public MarketInfrastructureModule(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        // Data Base
        var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
        builder.RegisterInstance(mongoDbSettings).As<MongoDbSettings>().SingleInstance();
        builder.RegisterType<MarketDbContext>();
    }
}
