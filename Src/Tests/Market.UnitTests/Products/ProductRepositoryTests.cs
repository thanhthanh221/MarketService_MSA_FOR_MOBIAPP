using Market.Domain.Products;

namespace Market.UnitTests.Products;

[Collection("MongoDbCollection")]
public class ProductRepositoryTests : IClassFixture<ProductUnitTestsFixture>
{
    private readonly ProductUnitTestsFixture fixture;
    public ProductRepositoryTests(ProductUnitTestsFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task GetProductById_Product_InRepositoty()
    {
        var product = ProductUnitTestsFixture.CreateProductDataFake();
        await fixture.productRepository.CreateProductAsync(product);

        var productAggregateInDataBase = await fixture.productRepository.GetProductByIdAsync(product.ProductId);

        Assert.Equal(product.ProductId.Id, productAggregateInDataBase.ProductId.Id);
        Assert.Equal(product.Categories.Count, productAggregateInDataBase.Categories.Count);
    }

    [Fact]
    public async Task UpdateProduct_Product_InRepository()
    {
        var product = ProductUnitTestsFixture.CreateProductDataFake();
        await fixture.productRepository.CreateProductAsync(product);

        product.ProductRemoved(product.ProductId);
        await fixture.productRepository.UpdateProductAsync(product);

        var productInDataBase = await fixture.productRepository.GetProductByIdAsync(product.ProductId);

        Assert.Equal(productInDataBase.ProductId.Id, product.ProductId.Id);
        Assert.Equal(productInDataBase.ProductStatus.StatusValue, ProductStatus.Remove.StatusValue);
    }

}
