using Market.Domain.Products;

namespace Market.Domain.ProductComments;
public interface IProductCommentRepository
{
    Task CreateProductCommentAsync(ProductCommentAggregate productComment);
    Task<ProductCommentAggregate> GetProductCommentByIdAsync(ProductCommentId productCommentId);
    Task<List<ProductCommentAggregate>> GetCommentsByProductIdAsync(ProductId productId);
    Task DeleteProductComment(ProductCommentId productCommentId);
    Task UpdateProductComment(ProductCommentAggregate productComment);
}
