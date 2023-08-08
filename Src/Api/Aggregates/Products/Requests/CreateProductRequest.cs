using System.ComponentModel.DataAnnotations;
using Market.Application.Products.Commands.CreateProduct;
using Market.Domain.Products;
using Newtonsoft.Json;

namespace Market.Api.Aggregates.Product.Requests;
public class CreateProductRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int Calo { get; set; }
    [Required]
    public string Descretion { get; set; }
    [Required]
    public string ProductTypeName { get; set; }
    [Required]
    public List<string> ProductTypeValuesRequestData { get; set; }
    [Required]
    public List<int> CategoriesId { get; set; }
    [Required]
    public int Hour { get; set; }
    [Range(0, 60)]
    [Required]
    public int Minute { get; set; }
    [Required]
    public IFormFile ProductImageUri { get; set; }

    public CreateProductCommand ConvertRequestDataToCommand(string productImageUri)
    {
        List<ProductTypeValue> productTypeValue = ProductTypeValuesRequestData
            .Select(p => ProductTypeValueRequest.ConvertStringToProductValueEntity(p)).ToList();


        return new(Guid.NewGuid(), Name, Calo, Descretion, productImageUri,
            DateTime.UtcNow, ProductTypeName,
            productTypeValue, CategoriesId, new(Hour, Minute, 0));

    }

}

public class ProductTypeValueRequest
{
    public string ValueType { get; set; }
    public decimal PriceType { get; set; }
    public int QuantityType { get; set; }

    public static ProductTypeValue ConvertStringToProductValueEntity(string JsonValue)
    {
        var productTypeValueRequest = JsonConvert.DeserializeObject<ProductTypeValueRequest>(JsonValue);

        ProductTypeValueId productTypeValueId = new(Guid.NewGuid());

        return new ProductTypeValue(productTypeValueId, productTypeValueRequest.ValueType,
            productTypeValueRequest.PriceType, productTypeValueRequest.QuantityType);
    }
}
