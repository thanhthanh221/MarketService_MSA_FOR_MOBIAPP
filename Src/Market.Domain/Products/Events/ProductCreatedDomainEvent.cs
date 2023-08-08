using Market.Domain.Core;

namespace Market.Domain.Products.Events;
public class ProductCreatedDomainEvent : DomainEventBase
{
    public ProductCreatedDomainEvent(ProductAggregate productAggregate)
    {
        ProductAggregate = productAggregate;
    }
    public ProductAggregate ProductAggregate { get; set; }
}


