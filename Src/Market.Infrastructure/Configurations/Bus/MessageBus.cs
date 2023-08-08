using Market.Application.Common.Bus;
using MediatR;

namespace Market.Infrastructure.Configurations.Bus;
public class MessageBus : IMessageBus
{
    private readonly IMediator mediator;

    public MessageBus(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task Publish<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : INotification
    {
        await mediator.Publish(@event, cancellationToken);
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        return await mediator.Send(request, cancellationToken);
    }
}
