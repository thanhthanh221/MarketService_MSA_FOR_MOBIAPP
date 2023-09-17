using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Products.Events;
public class BoughtProductDomainEvent : DomainEventBase
{
    public UserId UserId { get; private set; }
    public ProductId ProductId { get; private set; }
    public ProductTypeBoughtEvent ProductTypeBought { get; private set; }

    public BoughtProductDomainEvent(UserId userId, ProductId productId, ProductTypeBoughtEvent productTypeBought)
    {
        ProductId = productId;
        ProductTypeBought = productTypeBought;
        UserId = userId;
    }
}

public record ProductTypeBoughtEvent(
    ProductTypeValueId ValueTypeOrderId,
    decimal PriceProductType,
    int CountOrder);
