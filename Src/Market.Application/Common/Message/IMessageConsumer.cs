using RabbitMQ.Client;

namespace Market.Application.Common.Message;
public interface IMessageConsumer<T>
{
    Task<T> Consume(IModel channel);
}
