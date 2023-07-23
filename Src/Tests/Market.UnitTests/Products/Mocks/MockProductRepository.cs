using Market.Domain.Products;
using Market.Domain.Users;
using Moq;

namespace Market.UnitTests.Products.Mocks;
public static class MockProductRepository
{
    public static Mock<IProductRepository> GetProductRepository() {
        var product1 = CreateProductDataFake();
        var product2 = CreateProductDataFake();

        List<ProductAggregate> products = new() {
            product1,
            product2
        };

        Mock<IProductRepository> mockProductRepo = new();

        mockProductRepo.Setup(r => 
            r.GetProductByIdAsync(product1.ProductId)).ReturnsAsync(product1);

        mockProductRepo.Setup(r => r.CreateProductAsync(It.IsAny<ProductAggregate>()))
            .Returns((ProductAggregate product ) => {
                products.Add(product);
                return products;
            });
        
        return mockProductRepo;
    }

    public static ProductAggregate CreateProductDataFake()
    {
        ProductId productId = new(Guid.NewGuid());
        ProductInfomation productInfomation = new(Guid.NewGuid().ToString()[..11], 1000, 10, Guid.NewGuid().ToString()[..5], 3, "https://eatcleanhub.com/wp-content/uploads/2022/02/bim1.jpg", DateTime.UtcNow);
        ProductType productType = new(Guid.NewGuid().ToString(),
        new List<ProductTypeValue>(){
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),100,27),
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),20,63),
            new ProductTypeValue(new ProductTypeValueId(Guid.NewGuid()),Guid.NewGuid().ToString(),35,58),

        });
        List<ProductCategory> productCategories = new() {
            ProductCategory.Bbq,
            ProductCategory.Bread
        };
        ProductUser productUser = new() {
            UserFavouriteProduct = new List<UserId>() {
                new UserId(Guid.NewGuid()),
                new UserId(Guid.NewGuid())
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
        
        return product;
    }

}
