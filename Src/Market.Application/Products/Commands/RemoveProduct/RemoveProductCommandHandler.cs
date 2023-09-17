using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Domain.Products;
using Market.Domain.Products.Events;

namespace Market.Application.Products.Commands.RemoveProduct;
public class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand, Guid>
{
    private readonly IProductRepository productRepository;
    private readonly IMessageBus messageBus;

    public RemoveProductCommandHandler(IProductRepository productRepository, IMessageBus messageBus)
    {
        this.productRepository = productRepository;
        this.messageBus = messageBus;
    }

    public async Task<Guid> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.ProductId);
        if (product is null) return Guid.Empty;

        product.RemoveProduct(product.ProductId);
        await productRepository.UpdateProductAsync(product);

        await messageBus.Publish(new RemovedProductDomainEvent(request.ProductId), cancellationToken);
        return request.ProductId.Id;
    }
}
