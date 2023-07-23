using Market.Application.Configurations.Commands;
using Market.Domain.Products;

namespace Market.Application.Products.Commands.RemoveProduct;
public class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand, Guid>
{
    private readonly IProductRepository productRepository;

    public RemoveProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Guid> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.ProductId);
        if (product is null) return Guid.Empty;

        product.ProductRemoved(product.ProductId);
        await productRepository.UpdateProductAsync(product);
        return request.ProductId.Id;
    }
}
