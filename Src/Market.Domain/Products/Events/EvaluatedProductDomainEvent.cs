using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Products.Events;
public class EvaluatedProductDomainEvent : DomainEventBase
{
    public UserId UserId { get; set; }
    public ProductId ProductId { get; set; }
    public double NewStar { get; set; }

    public EvaluatedProductDomainEvent(UserId userId, ProductId productId, double newStar)
    {
        UserId = userId;
        ProductId = productId;
        NewStar = newStar;
    }
}
