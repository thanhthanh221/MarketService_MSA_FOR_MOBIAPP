using Market.Domain.Products;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Market.Infrastructure.Domain.Products;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<ProductAggregate> productCollection;
    private readonly FilterDefinitionBuilder<ProductAggregate> filterBuilder = Builders<ProductAggregate>.Filter;

    public ProductRepository(IMongoCollection<ProductAggregate> productCollection)
    {
        this.productCollection = productCollection;
    }

    public async Task CreateProductAsync(ProductAggregate productAggregate)
    {
        await productCollection.InsertOneAsync(productAggregate);
    }

    public async Task<List<ProductAggregate>> GetAllProductAsync()
    {
        return await productCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<ProductAggregate> GetProductByIdAsync(ProductId productId)
    {
        var filter = filterBuilder?.Eq(p => p.ProductId, productId);
        return await productCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<ProductAggregate>> GetProductPagingAsync(int Page, int PageSize)
    {
        var allProduct = await productCollection.Find(new BsonDocument()).ToListAsync();
        return allProduct.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
    }

    public async Task RemoveProductAsync(ProductId productId)
    {
        var filter = filterBuilder?.Eq(p => p.ProductId, productId);
        await productCollection.DeleteOneAsync(filter);
    }
    
    public async Task UpdateProductAsync(ProductAggregate productAggregate)
    {
        var filter = filterBuilder?.Eq(p => p.ProductId, productAggregate.ProductId);
        await productCollection.ReplaceOneAsync(filter, productAggregate);
    }
}
