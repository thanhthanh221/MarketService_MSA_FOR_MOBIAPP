using Event.Message.CreateOrder;
using Event.Message.CreateOrder.CustomerService;
using Event.Message.CreateOrder.MarketService.Coupons;
using Event.Message.CreateOrder.MarketService.Products;
using Event.Message.CreateOrder.OrderService;
using GreenPipes;
using Market.Application.Coupons.Consumers;
using Market.Application.Products.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Market.Infrastructure.Dependency;
public class RabbitMqDependency : IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((context, configurator) =>
                {
                    IConfiguration Configuration = context.GetService<IConfiguration>();

                    configurator.Host("localhost");
                    configurator.UseMessageRetry(entryConfigurator =>
                    {
                        entryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                    });

                    configurator.Message<ICreatedOrderMessageEvent>(e =>
                    {
                        e.SetEntityName("create-order-exchange");
                    });
                    configurator.Publish<ICreatedOrderMessageEvent>(e =>
                    {
                        e.ExchangeType = ExchangeType.Topic;
                    });
                    configurator.Send<ICreatedOrderMessageEvent>(e =>
                    {
                        e.UseRoutingKeyFormatter(context => context.Message.Type);
                    });


                    configurator.ReceiveEndpoint("create-order-product-queue", e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        e.Bind("create-order-exchange", e =>
                        {
                            e.RoutingKey = nameof(CreatedOrderEvent).ToString();
                            e.ExchangeType = ExchangeType.Topic;
                        });
                        e.Consumer<CreatedOrderProductServiceConsumer>();
                    });
                    configurator.ReceiveEndpoint("create-order-rollback-product-queue", e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        e.Bind("create-order-exchange", e =>
                        {
                            e.RoutingKey = nameof(CreatedOrderFailCouponServiceEvent).ToString();
                            e.ExchangeType = ExchangeType.Topic;
                        });
                        e.Consumer<CreatedOrderRollBackProductConsumer>();
                    });


                    configurator.ReceiveEndpoint("create-order-coupon-queue", e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        e.Bind("create-order-exchange", e =>
                        {
                            e.RoutingKey = nameof(CreatedOrderProductServiceEvent).ToString();
                            e.ExchangeType = ExchangeType.Topic;
                        });
                        e.Consumer<CreatedOrderCouponServiceConsumer>();
                    });
                    configurator.ReceiveEndpoint("create-order-rollback-coupon-queue", e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        e.Bind("create-order-exchange", e =>
                        {
                            e.RoutingKey = nameof(CreatedOrderFailCustomerEvent).ToString();
                            e.ExchangeType = ExchangeType.Topic;
                        });
                        e.Consumer<CreatedOrderRollBackCouponConsumer>();
                    });
                });
            });
        services.AddMassTransitHostedService();
    }
}
