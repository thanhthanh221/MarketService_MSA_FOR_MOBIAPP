namespace Market.Domain.Core;
public class DomainEventBase : IDomainEvent
{
    public Guid Id { get; }

    public DateTime OccurredOn { get; }

    public DomainEventBase()
    {
        (Id, OccurredOn) = (Guid.NewGuid(), DateTime.UtcNow);
    }
}
