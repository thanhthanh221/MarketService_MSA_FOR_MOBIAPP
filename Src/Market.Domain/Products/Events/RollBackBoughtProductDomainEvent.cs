using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Products.Events;
public class RollBackBoughtProductDomainEvent : DomainEventBase
{
    public ProductId ProductId { get; private set; }
    public UserId UserId { get; private set; }
    public ProductTypeBoughtEvent ProductTypeBought { get; private set; }

    public RollBackBoughtProductDomainEvent(
        ProductId productId, UserId userId, ProductTypeBoughtEvent productTypeBought)
    {
        ProductId = productId;
        UserId = userId;
        ProductTypeBought = productTypeBought;
    }
}
