using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using Market.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Market.Application.Products.Commands.BuyProduct;
public class BuyProductCommandHandler : ICommandHandler<BuyProductCommand, bool>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<BuyProductCommandHandler> logger;
    private readonly IMessageBus messageBus;

    public BuyProductCommandHandler(
        IProductRepository productRepository, ILogger<BuyProductCommandHandler> logger, IMessageBus messageBus)
    {
        this.productRepository = productRepository;
        this.logger = logger;
        this.messageBus = messageBus;
    }

    public Task<bool> Handle(BuyProductCommand request, CancellationToken cancellationToken)
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

            product.BuyProduct(productTypeValueId, p.CountOrder);

            await productRepository.UpdateProductAsync(product);

            ProductTypeBoughtEvent typeBoughtEvent = new(
                productTypeValueId, productTypeValue.PriceType, p.CountOrder);

            await messageBus.Publish(new BoughtProductDomainEvent(productId, typeBoughtEvent));

            string message = $"User:Order Product: {productId} with type:{productTypeValueId}";
            logger.LogInformation(message);
        });
        return Task.FromResult(true);
    }
}
