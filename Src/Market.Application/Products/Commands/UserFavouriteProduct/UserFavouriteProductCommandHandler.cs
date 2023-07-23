using Market.Application.Configurations.Commands;
using Market.Domain.Products;
using Microsoft.Extensions.Logging;

namespace Market.Application.Products.Commands.UserFavouriteProduct;
public class UserFavouriteProductCommandHandler : ICommandHandler<UserFavouriteProductCommand, bool>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<UserFavouriteProductCommandHandler> logger;

    public UserFavouriteProductCommandHandler(IProductRepository productRepository, ILogger<UserFavouriteProductCommandHandler> logger)
    {
        this.productRepository = productRepository;
        this.logger = logger;
    }

    public async Task<bool> Handle(UserFavouriteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.ProductId);
        if (product is null) return false;

        var checkUserFavourite = product.ProductUser.UserFavouriteProduct.Any(u => u == request.User);

        if (checkUserFavourite) {
            product.UserRemovedFavourite(product.ProductId, request.User);
            logger.LogInformation($"{request.User} Un favourite froduct {request.ProductId}");
        }
        product.UserFavouriteProduct(product.ProductId, request.User);
        logger.LogInformation($"{request.User} favourite product {request.ProductId}");

        await productRepository.UpdateProductAsync(product);
        return true;
    }
}
