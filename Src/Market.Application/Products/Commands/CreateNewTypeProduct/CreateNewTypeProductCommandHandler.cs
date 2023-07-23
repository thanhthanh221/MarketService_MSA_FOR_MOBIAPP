using Market.Application.Configurations.Commands;
using Market.Application.Products.Commands.AddNewTypeProduct;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Market.Application.Products.Commands.CreateNewTypeProduct;

public class CreateNewTypeProductCommandHandler : ICommandHandler<CreateNewTypeProductCommand, Guid>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<CreateNewTypeProductCommandHandler> logger;
    private readonly IMediator mediator;

    public CreateNewTypeProductCommandHandler(IProductRepository productRepository, ILogger<CreateNewTypeProductCommandHandler> logger, IMediator mediator)
    {
        this.productRepository = productRepository;
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task<Guid> Handle(CreateNewTypeProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.ProductId);
        var newListProductTypeAdd = request.ProductTypeValues;

        product.CreatedNewProductType(request.AdminId, newListProductTypeAdd);

        await productRepository.UpdateProductAsync(product);

        logger.LogInformation(
            $"Admin: {request.AdminId} Add new product type: {newListProductTypeAdd} For product: {request.ProductId}");

        await mediator.Publish(new ProductCreatedNewProductTypeDomainEvent(
            request.ProductId, request.AdminId, newListProductTypeAdd), cancellationToken);
        return product.ProductId.Id;
    }
}
