using Market.Domain.Products;
using MongoDB.Bson.Serialization;

namespace Market.Infrastructure.Domain.Products;
public static class ProductConfiguration
{
    public static void Configure(BsonClassMap<ProductAggregate> p)
    {
        p.AutoMap();
        p.SetIgnoreExtraElements(true);
        // // Key
        p.MapIdMember(x => x.ProductId);

        // Product Infomation
        // p.MapProperty(p => p.ProductInfomation.Name).SetElementName("Product Name").SetIsRequired(true);
        // p.MapProperty(p => p.ProductInfomation.Price).SetElementName("Product Price").SetIsRequired(true);
        // p.MapProperty(p => p.ProductInfomation.Calo).SetElementName("Product Calo").SetIsRequired(true);
        // p.MapProperty(p => p.ProductInfomation.Descretion).SetElementName("Product Descretion").SetIsRequired(true);
        // p.MapProperty(p => p.ProductInfomation.Star).SetElementName("Product Star").SetIsRequired(true);
        // p.MapProperty(p => p.ProductInfomation.ProductImageUri).SetElementName("Product Image Uri").SetIsRequired(true);
        // p.MapProperty(p => p.ProductInfomation.CreateAt).SetElementName("Time Create").SetIsRequired(true);

        // // Product Type
        // p.MapProperty(p => p.ProductType.ProductTypeName).SetElementName("Product Type Name").SetIsRequired(true);
        // p.MapProperty(p => p.ProductType.ProductTypeValues).SetElementName("Product Type Infomations").SetIsRequired(true);

        // // Product Status
        // p.MapProperty(p => p.ProductStatus).SetElementName("Product Status").SetIsRequired(true);

        // // Product User
        // p.MapProperty(p => p.ProductUser.UserFavouriteProduct).SetElementName("User Like Product").SetIgnoreIfDefault(true);

        // // Product Order
        // p.MapProperty(p => p.ProductOrder.CountOrder)
        //     .SetElementName("Count Product Sold")
        //     .SetDefaultValue(0).SetIsRequired(true);

        // p.MapProperty(p => p.ProductOrder.TimeOrder)
        //     .SetElementName("Time Order Product")
        //     .SetIsRequired(true);
    }
}
