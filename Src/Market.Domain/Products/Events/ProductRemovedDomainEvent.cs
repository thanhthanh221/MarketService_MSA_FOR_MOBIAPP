using Market.Domain.Core;

namespace Market.Domain.Products.Events;
public class ProductRemovedDomainEvent : DomainEventBase
{
    public ProductId ProductId { get; set; }
    public ProductRemovedDomainEvent(ProductId productId)
    {
        ProductId = productId;
    }
}
