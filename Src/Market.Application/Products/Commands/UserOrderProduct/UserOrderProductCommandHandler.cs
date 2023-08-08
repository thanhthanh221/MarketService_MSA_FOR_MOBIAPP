using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using Market.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Market.Application.Products.Commands.UserOrderProduct;
public class UserOrderProductCommandHandler : ICommandHandler<UserOrderProductCommand, bool>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<UserOrderProductCommandHandler> logger;
    private readonly IMessageBus messageBus;

    public UserOrderProductCommandHandler(
        IProductRepository productRepository, ILogger<UserOrderProductCommandHandler> logger, IMessageBus messageBus)
    {
        this.productRepository = productRepository;
        this.logger = logger;
        this.messageBus = messageBus;
    }

    public Task<bool> Handle(UserOrderProductCommand request, CancellationToken cancellationToken)
    {
        UserId userId = new(request.UserId);

        request.ProductOrderDataToCommands.ForEach(async p =>
        {
            ProductId productId = new(p.ProductId);
            ProductTypeValueId productTypeValueId = new(p.ProductTypeValueId);

            var product = await productRepository.GetProductByIdAsync(productId);
            if (product is null) return;

            ProductTypeValue productTypeValue = product
                .ProductType.GetProductTypeByProductTypeId(productTypeValueId);

            if (productTypeValue is null) return;

            product.UserOrderProductSuccess(userId, productTypeValueId, p.CountOrder);

            await productRepository.UpdateProductAsync(product);

            ProductTypeUserOrderEvent typeOrderEvent = new(
                productTypeValueId, productTypeValue.ValueType, productTypeValue.PriceType, p.CountOrder);

            await messageBus.Publish(new
                ProductUserOrderedProductSuccessDomainEvent(productId, userId, typeOrderEvent));

            string message = $"User:Order Product: {productId} with type:{productTypeValueId}";
            logger.LogInformation(message);
        });
        return Task.FromResult(true);
    }
}
