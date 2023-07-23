using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace Market.Domain.Core;

public abstract class Entity
{
    [BsonIgnore]  // If Using MongoDb 
    [NotMapped]   // If Using Entity Framework
    private List<IDomainEvent> _domainEvents;
    public long Version { get; private set; } = 0;

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();

        _domainEvents.Add(domainEvent);
        
        Version++;
    }

    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken()) {
            throw new BusinessRuleValidationException(rule);
        }
    }

}
