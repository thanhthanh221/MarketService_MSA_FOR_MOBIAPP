using Market.Application.Products.Queries.GetProductById;
using Market.Domain.Users;
using MediatR;

namespace Market.IntegrationTests.Products;
public class GetProductByIdIntegrationTests : IClassFixture<ProductIntergrationTestsFixture>
{
    private readonly ProductIntergrationTestsFixture fixture;
    private readonly IMediator mediator;
    public GetProductByIdIntegrationTests(
        ProductIntergrationTestsFixture fixture, IMediator mediator)
    {
        this.fixture = fixture;
        this.mediator = mediator;
    }

    
    [Fact]
    public async Task GetProductById_Product_InRepositoty()
    {
        var product = ProductIntergrationTestsFixture.CreateProductDataFake();
        await fixture.productRepository.CreateProductAsync(product);

        var productAggregateInDataBase = await fixture.productRepository.GetProductByIdAsync(product.ProductId);

        Assert.Equal(product.ProductId.Id, productAggregateInDataBase.ProductId.Id);
        Assert.Equal(product.Categories.Count, productAggregateInDataBase.Categories.Count);
    }
}
