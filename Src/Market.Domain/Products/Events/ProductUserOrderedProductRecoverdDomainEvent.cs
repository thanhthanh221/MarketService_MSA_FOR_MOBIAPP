using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Products.Events;
public class ProductUserOrderedProductRecoverdDomainEvent : DomainEventBase
{
    public ProductId ProductId { get; private set; }
    public UserId UserId { get; private set; }
    public ProductTypeUserOrderEvent ProductTypeUserOrder { get; private set; }

    public ProductUserOrderedProductRecoverdDomainEvent(
        ProductId productId, UserId userId, ProductTypeUserOrderEvent productTypeUserOrder)
    {
        ProductId = productId;
        UserId = userId;
        ProductTypeUserOrder = productTypeUserOrder;
    }

}
