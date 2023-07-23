using Market.Domain.Products;
using Market.Domain.Users;

namespace Market.IntegrationTests.Products;
public class CreateProductIntegrationTests : IClassFixture<ProductIntergrationTestsFixture>
{
    private readonly ProductIntergrationTestsFixture fixture;
    public CreateProductIntegrationTests(ProductIntergrationTestsFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task Shoud_Succed_When_CreateProduct()
    {

        var product = ProductIntergrationTestsFixture.CreateProductDataFake();
        await fixture.productRepository.CreateProductAsync(product);

        var productAggregateInDataBase = await fixture.productRepository.GetProductByIdAsync(product.ProductId);

        Assert.Equal(product.ProductId.Id, productAggregateInDataBase.ProductId.Id);
        Assert.Equal(product.Categories.Count, productAggregateInDataBase.Categories.Count);
    }

}
