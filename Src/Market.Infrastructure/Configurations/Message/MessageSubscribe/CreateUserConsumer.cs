using System.Text;
using Market.Application.Common.Message;
using Market.Domain.Users;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Market.Infrastructure.Configurations.Message.MessageSubscribe;
public class CreateUserConsumer : BackgroundService
{
    private readonly IUserRepository userRepository;
    private readonly IModel channel;

    public CreateUserConsumer(IUserRepository userRepository, IModel channel)
    {
        this.userRepository = userRepository;
        this.channel = channel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        UserMessageBroker userMessageBroker = new();
        channel.ExchangeDeclare(
            RabbitMqExchangePattern.IdentityServiceFanoutExchange, ExchangeType.Fanout);

        channel.QueueDeclare(RabbitMqQueuePattern.MarketServiceCreateUserQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
        channel.QueueBind(RabbitMqQueuePattern.MarketServiceCreateUserQueue,
            RabbitMqExchangePattern.IdentityServiceFanoutExchange, string.Empty);
        channel.BasicQos(0, 10, false); // chỉ nhận 1 lúc tối đa 10 tin nhắn từ server

        EventingBasicConsumer consumer = new(channel);

        consumer.Received += (sender, e) => {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            userMessageBroker = JsonConvert.DeserializeObject<UserMessageBroker>(message);
        };
        
        UserAggregate userAggregate = 
            new(userMessageBroker.UserId, userMessageBroker.UserName, userMessageBroker.AvarUri);
        await userRepository.CreateUserAsync(userAggregate);

        channel.BasicConsume(RabbitMqQueuePattern.MarketServiceCreateUserQueue, true, consumer);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        channel.Close();
        await base.StopAsync(cancellationToken);
    }
}
