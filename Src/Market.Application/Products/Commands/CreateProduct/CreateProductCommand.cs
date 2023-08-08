using Market.Application.Contracts;
using Market.Domain.Products;

namespace Market.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : CommandBase<Guid>
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Calo { get; set; }
    public string Descretion { get; set; }
    public double Star { get; set; }
    public string ProductImageUri { get; set; }
    public DateTime CreateAt { get; set; }
    public string ProductTypeName { get; set; }
    public List<ProductTypeValue> ProductTypeValues { get; set; }
    public List<int> CategoriesId { get; set; }
    public TimeSpan TimeOrder { get; set; }

    public CreateProductCommand(Guid productId, string name, int calo, string descretion, string productImageUri, DateTime createAt, string productTypeName, List<ProductTypeValue> productTypeValues, List<int> categoriesId, TimeSpan timeOrder)
    {
        ProductId = productId;
        Name = name;
        Calo = calo;
        Descretion = descretion;
        Star = 0;
        ProductImageUri = productImageUri;
        CreateAt = createAt;
        ProductTypeName = productTypeName;
        ProductTypeValues = productTypeValues;
        CategoriesId = categoriesId;
        TimeOrder = timeOrder;
    }
}
