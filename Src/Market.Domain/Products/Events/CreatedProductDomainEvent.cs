using Market.Domain.Core;

namespace Market.Domain.Products.Events;
public class CreatedProductDomainEvent : DomainEventBase
{
    public CreatedProductDomainEvent(ProductAggregate productAggregate)
    {
        ProductAggregate = productAggregate;
    }
    public ProductAggregate ProductAggregate { get; set; }
}


