using Market.Infrastructure.Configurations.Message.MessageSubscribe;
using Market.Infrastructure.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Market.Infrastructure.Dependency;
public class RabbitMqDependency : IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));
        services.AddSingleton(sp => {
            var rabbitMqSettings = sp.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
            ConnectionFactory factory = new() {
                HostName = rabbitMqSettings.Hostname,
                UserName = rabbitMqSettings.Username,
                Password = rabbitMqSettings.Password,
                Port = rabbitMqSettings.Port,
                VirtualHost = rabbitMqSettings.VirtualHost
            };
            return factory;
        });

        // Đăng ký IConnection
        services.AddSingleton(sp => {
            var factory = sp.GetRequiredService<ConnectionFactory>();
            var connection = factory.CreateConnection();
            return connection;
        });

        // Đăng ký IModel
        services.AddSingleton(sp => {
            var connection = sp.GetRequiredService<IConnection>();
            var channel = connection.CreateModel();
            return channel;
        });

        // Đăng Ký Consumer
        services.AddHostedService<CreateUserConsumer>();
    }
}
