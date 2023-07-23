using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Products.Events;
public class ProductUserOrderedProductSuccessDomainEvent : DomainEventBase
{
    public ProductId ProductId { get; private set; }
    public UserId UserId { get; private set; }
    public List<ProductTypeUserOrderEvent> ProductTypeUserOrders { get; private set; }
    public DateTime TimeOrderProduct { get; private set; }

    public ProductUserOrderedProductSuccessDomainEvent(
        ProductId productId, UserId userId, List<ProductTypeUserOrderEvent> productTypeUserOrders)
    {
        ProductId = productId;
        UserId = userId;
        ProductTypeUserOrders = productTypeUserOrders;
        TimeOrderProduct = DateTime.UtcNow;
    }
}

public record ProductTypeUserOrderEvent(
    ProductTypeValueId ValueTypeOrderId,
    string ValueTypeOrder,
    decimal PriceProductType,
    int CountOrder);
