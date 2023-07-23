using Market.Application.Configurations.Commands;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Market.Application.Products.Commands.OrderProduct;
public class OrderProductCommandHandler : ICommandHandler<OrderProductCommand, Guid>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<OrderProductCommandHandler> logger;
    private readonly IMediator mediator;

    public OrderProductCommandHandler(
        IProductRepository productRepository, ILogger<OrderProductCommandHandler> logger, IMediator mediator)
    {
        this.productRepository = productRepository;
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task<Guid> Handle(OrderProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.ProductId);
        if (product is null) return Guid.Empty;

        var productTypesOrder = request.ProductOrderItemCommands.Select(p => {
            var productTypeInDB = product.ProductType.GetProductTypeByProductTypeId(p.ProductTypeValueId);
            if (productTypeInDB is null) {
                return null;
            }
            return new ProductTypeUserOrderEvent(
                p.ProductTypeValueId, productTypeInDB.ValueType, productTypeInDB.PriceType, p.CountOrder);
        });
        product.UserOrderProductSuccess(request.UserId, productTypesOrder.ToList());

        await productRepository.UpdateProductAsync(product);
        logger.LogInformation(
            $"Update Product: {product.ProductId} Value When User: {request.UserId} Order Suscess");

        await mediator.Publish(new ProductUserOrderedProductSuccessDomainEvent(
            request.ProductId, request.UserId, productTypesOrder.ToList()), cancellationToken);

        return request.ProductId.Id;
    }
}
