using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Infrastructure.Dependency;
public interface IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration);
}
