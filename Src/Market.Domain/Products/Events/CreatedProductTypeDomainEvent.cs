using Market.Domain.Core;
using Market.Domain.Users;

namespace Market.Domain.Products.Events;
public class CreatedProductTypeDomainEvent : DomainEventBase
{
    public ProductId ProductId { get; set; }
    public UserId AdminId { get; set; }
    public List<ProductTypeValue> NewProductType { get; set; }

    public CreatedProductTypeDomainEvent(
        ProductId productId, UserId adminId, List<ProductTypeValue> newProductType)
    {
        ProductId = productId;
        AdminId = adminId;
        NewProductType = newProductType;
    }

}
