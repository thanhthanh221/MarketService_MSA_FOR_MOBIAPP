using Market.Application.Contracts;
using Market.Domain.Products;
using Market.Domain.Users;

namespace Market.Application.Products.Commands.CreateProductType;

public class CreateProductTypeCommand : CommandBase<Guid>
{
    public ProductId ProductId { get; private set; }
    public UserId AdminId { get; private set; }
    public List<ProductTypeValue> ProductTypeValues { get; private set; }

    public CreateProductTypeCommand(
        ProductId productId, UserId adminId, List<ProductTypeValue> productTypeValues)
    {
        ProductId = productId;
        AdminId = adminId;
        ProductTypeValues = productTypeValues;
    }

}
