using Market.Domain.Products.Exceptions;

namespace Market.Domain.Products;

public class ProductType
{
    public string ProductTypeName { get; private set; }
    public List<ProductTypeValue> ProductTypeValues { get; private set; }

    public ProductType(string productTypeName, List<ProductTypeValue> productTypeValues)
    {
        ProductTypeName = productTypeName;
        ProductTypeValues = productTypeValues;
    }

    public ProductTypeValue GetProductTypeByProductTypeId(ProductTypeValueId productTypeValueId)
    {
        return ProductTypeValues.FirstOrDefault(p => p.ProductTypeValueId.Equals(productTypeValueId));
    }
    public void SetProductType(List<ProductTypeValue> newProductTypesValue) {
        ProductTypeValues = newProductTypesValue;
    }
}

public class ProductTypeValue
{
    public ProductTypeValueId ProductTypeValueId { get; private set; }
    public string ValueType { get; private set; }
    public decimal PriceType { get; private set; }
    public int QuantityType { get; private set; }
    public int QuantityProductTypeSold { get; private set; }

    public ProductTypeValue(ProductTypeValueId productTypeValueId, string valueType, decimal priceType, int quantityType)
    {
        ProductTypeValueId = productTypeValueId;
        ValueType = valueType;
        PriceType = priceType;
        QuantityType = quantityType;
        QuantityProductTypeSold = 0;
    }

    public ProductTypeValue(
        ProductTypeValueId productTypeValueId, string valueType, decimal priceType, int quantityType, int quantityProductTypeSold)
        : this(productTypeValueId, valueType, priceType, quantityType)
    {
        QuantityProductTypeSold = quantityProductTypeSold;
    }

    public void SetQuantityProductType(int newQuantity)
    {
        if (newQuantity < 0) {
            throw new ProductQuantityLessThanZero();
        }
        QuantityType = newQuantity;
    }

    public void SetQuantityProductTypeSold(int newQuantityTypeSold)
    {
        QuantityProductTypeSold = newQuantityTypeSold;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())return false;
        ProductTypeValue otherObject = (ProductTypeValue)obj;
        return ValueType == otherObject.ValueType;
    }
    public override int GetHashCode()
    {
        return $"{ProductTypeValueId}+{ValueType}".GetHashCode();
    }

}
public class ProductTypeValueId
{
    public ProductTypeValueId(Guid typeId) => TypeId = typeId;

    public Guid TypeId { get; private set; }
}
