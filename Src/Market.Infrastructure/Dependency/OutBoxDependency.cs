using Market.Application.Common.OutBox;
using Market.Infrastructure.Configurations.OutBox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Infrastructure.Dependency;
public class OutBoxDependency : IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOutBox, OutboxAccessor>();
    }
}
