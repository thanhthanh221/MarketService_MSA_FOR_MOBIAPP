using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Products.Events;
public class ProductUserOrderedProductSuccessDomainEvent : DomainEventBase
{
    public ProductId ProductId { get; private set; }
    public UserId UserId { get; private set; }
    public ProductTypeUserOrderEvent ProductTypeUserOrder { get; private set; }

    public ProductUserOrderedProductSuccessDomainEvent(
        ProductId productId, UserId userId, ProductTypeUserOrderEvent productTypeUserOrder)
    {
        ProductId = productId;
        UserId = userId;
        ProductTypeUserOrder = productTypeUserOrder;
    }
}

public record ProductTypeUserOrderEvent(
    ProductTypeValueId ValueTypeOrderId,
    string ValueTypeOrder,
    decimal PriceProductType,
    int CountOrder);
