using Market.Domain.Core;
using MediatR;

namespace Market.Application.Configurations.Events;
public interface IEventHandler<in Event> : INotificationHandler<@Event> where @Event : DomainEventBase
{

}
