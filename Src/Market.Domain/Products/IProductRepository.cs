namespace Market.Domain.Products;

public interface IProductRepository
{
    Task CreateProductAsync(ProductAggregate productAggregate);
    Task RemoveProductAsync(ProductId productId);
    Task<List<ProductAggregate>> GetAllProductAsync();
    Task<ProductAggregate> GetProductByIdAsync(ProductId productId);
    Task<List<ProductAggregate>> GetProductPagingAsync(int Page, int PageSize);
    Task UpdateProductAsync(ProductAggregate productAggregate);
}
