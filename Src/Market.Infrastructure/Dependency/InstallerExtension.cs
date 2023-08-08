using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Infrastructure.Dependency;
public static class InstallerExtension
{
    public static void InstallerServiceInAssembly(this IServiceCollection services, IConfiguration configuration)
    {
        List<IInstaller> installer = typeof(IInstaller).Assembly.ExportedTypes
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

        installer.ForEach(install => install.Installer(services, configuration));
    }

}
