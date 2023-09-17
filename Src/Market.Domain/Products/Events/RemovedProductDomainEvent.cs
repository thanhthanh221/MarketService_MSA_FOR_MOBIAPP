using Market.Domain.Core;

namespace Market.Domain.Products.Events;
public class RemovedProductDomainEvent : DomainEventBase
{
    public ProductId ProductId { get; set; }
    public RemovedProductDomainEvent(ProductId productId)
    {
        ProductId = productId;
    }
}
