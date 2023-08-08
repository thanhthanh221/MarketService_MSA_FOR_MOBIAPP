using MediatR;

namespace Market.Application.Common.Bus;
public interface IMessageBus
{
    Task<TResponse> Send<TResponse>
        (IRequest<TResponse> request, CancellationToken cancellationToken = default);

    Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : INotification;
}
