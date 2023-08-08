using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Products.Events;
public class ProductEvaluatedByUserDomainEvent : DomainEventBase
{
    public UserId UserId { get; set; }
    public ProductId ProductId { get; set; }
    public double NewStar { get; set; }

    public ProductEvaluatedByUserDomainEvent(UserId userId, ProductId productId, double newStar)
    {
        UserId = userId;
        ProductId = productId;
        NewStar = newStar;
    }
}
