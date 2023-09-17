using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Products.Events;
public class UnLikedProductDomainEvent : DomainEventBase
{
    public UnLikedProductDomainEvent(ProductId productId, UserId userId)
    {
        ProductId = productId;
        UserId = userId;
    }

    public ProductId ProductId { get; set; }
    public UserId UserId { get; set; }

}
