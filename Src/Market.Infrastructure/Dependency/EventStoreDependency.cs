using Market.Domain.Coupons;
using Market.Domain.Products;
using Market.Infrastructure.EventSouring.Coupons;
using Market.Infrastructure.EventSouring.Products;
using Market.Infrastructure.EventSouring.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Infrastructure.Dependency;
public class EventStoreDependency : IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration)
    {
        var eventStoreSettings = configuration.GetRequiredSection(nameof(EventStoreSettings)).Get<EventStoreSettings>();
        services.AddEventStoreClient(eventStoreSettings.ConnectionString);

        services.AddScoped<IProductEventStore, ProductEventStore>();
        services.AddScoped<ICouponEventStore, CouponEventStore>();
        services.Configure<EventStoreSettings>(
            configuration.GetRequiredSection(nameof(EventStoreSettings)));
    }
}
