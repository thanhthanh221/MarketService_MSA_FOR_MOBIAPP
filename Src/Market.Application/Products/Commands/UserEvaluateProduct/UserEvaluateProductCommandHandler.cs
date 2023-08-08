using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using Market.Domain.Users;

namespace Market.Application.Products.Commands.UserEvaluateProduct;
public class UserEvaluateProductCommandHandler : ICommandHandler<UserEvaluateProductCommand, Guid>
{
    private readonly IProductRepository productRepository;
    private readonly IMessageBus messageBus;

    public UserEvaluateProductCommandHandler(
        IProductRepository productRepository, IMessageBus messageBus)
    {
        this.productRepository = productRepository;
        this.messageBus = messageBus;
    }

    public async Task<Guid> Handle(UserEvaluateProductCommand request, CancellationToken cancellationToken)
    {
        if (request.Star > 5 || request.Star < 0)
        {
            return Guid.Empty;
        }
        ProductId productId = new(request.ProductId);
        UserId userId = new(request.UserId);

        var product = await productRepository.GetProductByIdAsync(productId);
        if (product is null)
        {
            return Guid.Empty;
        }
        product.UserEvaluateProduct(request.Star, userId);
        
        await productRepository.UpdateProductAsync(product);
        
        await messageBus.Publish(new ProductEvaluatedByUserDomainEvent(
            userId,productId,product.ProductInfomation.Star), cancellationToken);

        return productId.Id;
    }
}
