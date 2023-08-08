using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Application.Products.Commands.UserEvaluateProduct;
using Market.Domain.ProductComments;
using Market.Domain.ProductComments.Events;

namespace Market.Application.ProductComment.Commands.CreateProductComment;
public class CreateProductCommentCommandHandler : ICommandHandler<CreateProductCommentCommand, Guid>
{
    private readonly IProductCommentRepository productCommentRepository;
    private readonly IMessageBus messageBus;

    public CreateProductCommentCommandHandler(
        IProductCommentRepository productCommentRepository, IMessageBus messageBus)
    {
        this.productCommentRepository = productCommentRepository;
        this.messageBus = messageBus;
    }

    public async Task<Guid> Handle(CreateProductCommentCommand request, CancellationToken cancellationToken)
    {
        ProductCommentAggregate productComment =
        new(request.ProductCommentId, request.ProductId.Id, request.UserId.Id, request.Comment, request.Star);

        await productCommentRepository.CreateProductCommentAsync(productComment);

        await messageBus.Publish(new ProductCommentCreatedByUserDomainEvent(productComment), cancellationToken);

        await messageBus.Send(
            new UserEvaluateProductCommand(request.UserId.Id, request.ProductId.Id, request.Star), cancellationToken);
        return productComment.ProductCommentId.Id;
    }
}
