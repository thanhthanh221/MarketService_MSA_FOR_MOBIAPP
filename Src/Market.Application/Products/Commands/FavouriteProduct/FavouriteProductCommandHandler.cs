using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using Microsoft.Extensions.Logging;

namespace Market.Application.Products.Commands.FavouriteProduct;
public class FavouriteProductCommandHandler : ICommandHandler<FavouriteProductCommand, bool>
{
    private readonly IProductRepository productRepository;
    private readonly ILogger<FavouriteProductCommandHandler> logger;
    private readonly IMessageBus messageBus;

    public FavouriteProductCommandHandler(IProductRepository productRepository,
        ILogger<FavouriteProductCommandHandler> logger, IMessageBus messageBus)
    {
        this.productRepository = productRepository;
        this.logger = logger;
        this.messageBus = messageBus;
    }

    public async Task<bool> Handle(FavouriteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductByIdAsync(request.ProductId);
        if (product is null) return false;

        var checkUserFavourite = product.ProductUser.UserFavouriteProduct.Any(u => u == request.User.Id);

        if (checkUserFavourite)
        {
            product.UnLikeProduct(request.User);
            logger.LogInformation($"{request.User} Un favourite froduct {request.ProductId}");

            await messageBus.Publish(new UnLikedProductDomainEvent(product.ProductId, request.User), cancellationToken);

            await productRepository.UpdateProductAsync(product);
            return true;

        }
        product.LikeProduct(request.User);
        logger.LogInformation($"{request.User} favourite product {request.ProductId}");

        await messageBus.Publish(new LikedProductDomainEvent(product.ProductId, request.User), cancellationToken);


        await productRepository.UpdateProductAsync(product);
        return true;
    }
}
