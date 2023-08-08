using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Products.Events;
public class ProductCreatedNewProductTypeDomainEvent : DomainEventBase
{
    public ProductId ProductId { get; set; }
    public UserId AdminId { get; set; }
    public List<ProductTypeValue> NewProductType { get; set; }

    public ProductCreatedNewProductTypeDomainEvent(
        ProductId productId, UserId adminId, List<ProductTypeValue> newProductType)
    {
        ProductId = productId;
        AdminId = adminId;
        NewProductType = newProductType;
    }

}
