using Market.Application.Configurations.Commands;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Market.Application.Products.Commands.CreateProductType;

public class CreateTypeProductCommandHandler : ICommandHandler<CreateProductTypeCommand, Guid>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<CreateTypeProductCommandHandler> logger;
    private readonly IMediator mediator;

    public CreateTypeProductCommandHandler(IProductRepository productRepository, ILogger<CreateTypeProductCommandHandler> logger, IMediator mediator)
    {
        this.productRepository = productRepository;
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task<Guid> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.ProductId);
        var newListProductTypeAdd = request.ProductTypeValues;

        product.CreateProductType(request.AdminId, newListProductTypeAdd);

        await productRepository.UpdateProductAsync(product);

        logger.LogInformation(
            $"Admin: {request.AdminId} Add new product type: {newListProductTypeAdd} For product: {request.ProductId}");

        await mediator.Publish(new CreatedProductTypeDomainEvent(
            request.ProductId, request.AdminId, newListProductTypeAdd), cancellationToken);
        return product.ProductId.Id;
    }
}
