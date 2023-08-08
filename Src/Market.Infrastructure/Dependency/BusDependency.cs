using Market.Application.Common.Bus;
using Market.Infrastructure.Configurations.Bus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Infrastructure.Dependency;
public class BusDependency : IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMessageBus, MessageBus>();
    }
}
