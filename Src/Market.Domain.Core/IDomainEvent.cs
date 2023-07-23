using MediatR;
namespace Market.Domain.Core;
public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}
