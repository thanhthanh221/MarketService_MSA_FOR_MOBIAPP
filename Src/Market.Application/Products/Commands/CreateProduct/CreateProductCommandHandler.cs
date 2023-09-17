using Market.Application.Configurations.Commands;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Market.Application.Products.Commands.CreateProduct;
internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository productRepository;
    private readonly IMediator mediator;
    private readonly ILogger<CreateProductCommandHandler> logger;

    public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator, ILogger<CreateProductCommandHandler> logger)
    {
        this.productRepository = productRepository;
        this.mediator = mediator;
        this.logger = logger;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Data for Product Aggregate
        ProductId productId = new(request.ProductId);
        ProductType productType = new(request.ProductTypeName, request.ProductTypeValues);

        // Min Price Product Value 
        decimal minPrice = request.ProductTypeValues.Min(p => p.PriceType);
        ProductInfomation productInfomation =
            new(request.Name, minPrice, request.Calo, request.Descretion, request.Star, request.ProductImageUri, DateTime.UtcNow);

        ProductUser productUser = new();
        ProductOrder productOrder = new(0, request.TimeOrder);
        List<ProductCategory> category = request.CategoriesId
            .Select(c => ProductCategory.GetCategoryById(c)).ToList();

        ProductAggregate product =
        new(productId, productInfomation, productType, ProductStatus.Active, category, productUser, productOrder);

        await productRepository.CreateProductAsync(product);
        logger.LogInformation($"Create Product: {product.ProductId.Id}");

        await mediator.Publish(new CreatedProductDomainEvent(product), cancellationToken);

        return product.ProductId.Id;
    }
}
