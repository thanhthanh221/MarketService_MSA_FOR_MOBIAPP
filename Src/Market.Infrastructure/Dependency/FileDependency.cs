using Market.Application.Common.File;
using Market.Infrastructure.Configurations.File;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Infrastructure.Dependency;
public class FileDependency : IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUploadFile, UploadFile>();
    }
}
