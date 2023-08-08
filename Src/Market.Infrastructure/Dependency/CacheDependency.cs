using Market.Application.Common.Cache;
using Market.Infrastructure.Configurations.Cache;
using Market.Infrastructure.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Market.Infrastructure.Dependency;
public class CacheDependency : IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration)
    {
        RedisSettings redisSettings = new();
        configuration.GetSection(nameof(RedisSettings)).Bind(redisSettings);

        services.AddSingleton(redisSettings);

        if (!redisSettings.Enable)
        {
            return;
        }
        services.AddSingleton<IConnectionMultiplexer>(_
            => ConnectionMultiplexer.Connect(redisSettings.ConnectionString));

        services.AddStackExchangeRedisCache(option => 
            option.Configuration = redisSettings.ConnectionString);

        services.AddScoped<IReposeCache, ReposeCache>();
    }
}
