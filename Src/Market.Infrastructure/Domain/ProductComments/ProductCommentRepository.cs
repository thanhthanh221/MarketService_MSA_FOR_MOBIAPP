using Market.Domain.ProductComments;
using Market.Domain.Products;
using MongoDB.Driver;

namespace Market.Infrastructure.Domain.ProductComments;
public class ProductCommentRepository : IProductCommentRepository
{
    private readonly IMongoCollection<ProductCommentAggregate> productCommentCollection;
    private readonly FilterDefinitionBuilder<ProductCommentAggregate> filterBuilder
        = Builders<ProductCommentAggregate>.Filter;

    public ProductCommentRepository(IMongoCollection<ProductCommentAggregate> productCommentCollection)
    {
        this.productCommentCollection = productCommentCollection;
    }
    public async Task CreateProductCommentAsync(ProductCommentAggregate productComment)
    {
        await productCommentCollection.InsertOneAsync(productComment);
    }

    public async Task DeleteProductComment(ProductCommentId productCommentId)
    {
        var filter = filterBuilder?.Eq(p => p.ProductCommentId, productCommentId);
        await productCommentCollection.DeleteOneAsync(filter);
    }

    public async Task<List<ProductCommentAggregate>> GetCommentsByProductIdAsync(ProductId productId)
    {
        var filter = filterBuilder?.Eq(p => p.ProductId, productId);
        return (await productCommentCollection.FindAsync(filter)).ToList();
    }

    public async Task<ProductCommentAggregate> GetProductCommentByIdAsync(ProductCommentId productCommentId)
    {
        var filter = filterBuilder?.Eq(p => p.ProductCommentId, productCommentId);
        return await productCommentCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateProductComment(ProductCommentAggregate productComment)
    {
        var filter = filterBuilder?.Eq(p => p.ProductCommentId, productComment.ProductCommentId);
        await productCommentCollection.ReplaceOneAsync(filter, productComment);
    }
}
