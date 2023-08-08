using Market.Domain.Products;
using Market.Domain.Products.Events;
using Market.Domain.Products.Exceptions;
using Market.Domain.Users;

namespace Market.UnitTests.Products;

public class ProductsUnitTests
{
    private readonly ProductAggregate productAggregate;

    public ProductsUnitTests()
    {
        ProductId productId = new(Guid.NewGuid());
        ProductInfomation productInfomation = new(Guid.NewGuid().ToString()[..11], 1000, 10, Guid.NewGuid().ToString()[..5], 3, "https://eatcleanhub.com/wp-content/uploads/2022/02/bim1.jpg", DateTime.UtcNow);
        ProductType productType = new(Guid.NewGuid().ToString(),
        new List<ProductTypeValue>(){
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),100,17),
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),20,86),
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),35,59),
        });
        List<ProductCategory> productCategories = new() {
            ProductCategory.Bbq,
            ProductCategory.Bread
        };
        ProductUser productUser = new() {
            UserFavouriteProduct = new() {
                Guid.NewGuid(),
                Guid.NewGuid()
            }
        };
        ProductOrder productOrder = new(0, new TimeSpan(1, 10, 10));

        ProductAggregate product = new(
            productId,
            productInfomation,
            productType, ProductStatus.Active,
            productCategories,
            productUser,
            productOrder
            );

        productAggregate = product;
    }

    [Fact]
    public void CreateProduct_IsSuccessful()
    {
        Assert.NotNull(productAggregate);
        Assert.Equal(2, productAggregate.Categories.Count);
        Assert.IsType<ProductCreatedDomainEvent>(productAggregate.DomainEvents.First());
    }

    [Fact]
    public void UserFavouriteProduct_IsSuccessful()
    {
        UserId userId = new(Guid.NewGuid());

        productAggregate.UserFavouriteProduct(userId);

        Assert.IsType<ProductUserFavouriteDomainEvent>(productAggregate.DomainEvents.LastOrDefault());
        Assert.Contains(userId.Id, productAggregate.ProductUser.UserFavouriteProduct);
    }

    [Fact]
    public void ThrowException_When_UserFavouriteProduct()
    {
        UserId userId = new(Guid.NewGuid());

        productAggregate.UserFavouriteProduct(userId);

        var exception = Assert.Throws<UserFavouriteProduct>(() =>
            productAggregate.UserFavouriteProduct(userId));

        Assert.Equal("User Has Favourited Product", exception.Message);
    }

    [Fact]
    public void UserUnFavouriteProduct_IsSuscess()
    {
        UserId userId = new(Guid.NewGuid());

        productAggregate.UserFavouriteProduct(userId);
        productAggregate.UserRemovedFavourite(productAggregate.ProductId, userId);

        Assert.IsType<ProductUserRemovedFavouriteDomainEvent>(productAggregate.DomainEvents.LastOrDefault());
        Assert.DoesNotContain(userId.Id, productAggregate.ProductUser.UserFavouriteProduct);
    }

    [Fact]
    public void ThrowException_When_UserUnFavouriteProduct()
    {
        UserId userId = new(Guid.NewGuid());

        var exception = Assert.Throws<UserDonotFavouriteProduct>(() =>
            productAggregate.UserRemovedFavourite(productAggregate.ProductId, userId));

        Assert.Equal("User Has Not Favourited Product", exception.Message);
    }

    [Fact]
    public void UserOrderProduct_IsSuscess()
    {
        UserId userId = new(Guid.NewGuid());

        productAggregate.UserOrderProductSuccess(
            userId, productAggregate.ProductType.ProductTypeValues[0].ProductTypeValueId,5);

        productAggregate.UserOrderProductSuccess(
            userId, productAggregate.ProductType.ProductTypeValues[1].ProductTypeValueId,5);

        ProductUserOrderedProductSuccessDomainEvent productDomainEventWhenUserOrderedProduct =
            (ProductUserOrderedProductSuccessDomainEvent)productAggregate.DomainEvents.LastOrDefault();

        Assert.IsType<ProductUserOrderedProductSuccessDomainEvent>(productAggregate.DomainEvents.LastOrDefault());
        Assert.Equal(userId, productDomainEventWhenUserOrderedProduct.UserId);

        Assert.Equal(12, productAggregate.ProductType.ProductTypeValues.First().QuantityType);
        Assert.Equal(81, productAggregate.ProductType.ProductTypeValues[1].QuantityType);

        Assert.Equal(5, productAggregate.ProductType.ProductTypeValues.First().QuantityProductTypeSold);
        Assert.Equal(5, productAggregate.ProductType.ProductTypeValues[1].QuantityProductTypeSold);
    }

    [Fact]
    public void UserOrderProduct_IsRecoverdSuscess()
    {
        UserId userId = new(Guid.NewGuid());

        productAggregate.UserOrderProductSuccess(
            userId, productAggregate.ProductType.ProductTypeValues[0].ProductTypeValueId,5);

        productAggregate.UserOrderRecoveredProduct(
            userId, productAggregate.ProductType.ProductTypeValues[0].ProductTypeValueId,5);
        Assert.IsType<ProductUserOrderedProductRecoverdDomainEvent>(productAggregate.DomainEvents.LastOrDefault());

        ProductUserOrderedProductRecoverdDomainEvent LastDomainEvent =
            (ProductUserOrderedProductRecoverdDomainEvent)productAggregate.DomainEvents.LastOrDefault();
        Assert.Equal(userId, LastDomainEvent.UserId);

        Assert.Equal(17, productAggregate.ProductType.ProductTypeValues.First().QuantityType);
        Assert.Equal(0, productAggregate.ProductType.ProductTypeValues.First().QuantityProductTypeSold);
    }

    [Fact]
    public void AdminAddNewProductType_Suscess()
    {
        UserId adminId = new(Guid.NewGuid());

        List<ProductTypeValue> newProductType = new(){
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),10,19),
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),20,85),
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),35,57),
        };

        productAggregate.CreatedNewProductType(adminId, newProductType);

        Assert.Equal(6, productAggregate.ProductType.ProductTypeValues.Count);
        Assert.IsType<ProductCreatedNewProductTypeDomainEvent>(productAggregate.DomainEvents.LastOrDefault());
    }

    [Fact]
    public void AdminAddNewProductType_When_ProductAlreadyExist_ProductType()
    {
        UserId adminId = new(Guid.NewGuid());

        List<ProductTypeValue> newProductType = new(){
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),10,19),
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),20,85),
            productAggregate.ProductType.ProductTypeValues[1],
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),
                productAggregate.ProductType.ProductTypeValues[1].ValueType,20,85),
        };

        productAggregate.CreatedNewProductType(adminId, newProductType);

        Assert.Equal(5, productAggregate.ProductType.ProductTypeValues.Count);
        Assert.IsType<ProductCreatedNewProductTypeDomainEvent>(productAggregate.DomainEvents.LastOrDefault());
    }

    [Fact]
    public void ThrowException_When_AdminAddProductTypes_ProductTypeAlreadyExist()
    {
        UserId adminId = new(Guid.NewGuid());

        List<ProductTypeValue> newProductType = new(){
            productAggregate.ProductType.ProductTypeValues[0],
            productAggregate.ProductType.ProductTypeValues[1],
            productAggregate.ProductType.ProductTypeValues[2],
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),
                productAggregate.ProductType.ProductTypeValues[1].ValueType,20,85),
        };

         var exception = Assert.Throws<AllAddedProductTypesAlreadyExist>(() =>
            productAggregate.CreatedNewProductType(adminId, newProductType));

        Assert.Equal("All added product types already exist", exception.Message);
    }
}
