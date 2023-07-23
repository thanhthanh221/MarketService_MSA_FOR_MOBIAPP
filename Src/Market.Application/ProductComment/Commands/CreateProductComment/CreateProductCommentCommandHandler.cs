using Market.Application.Configurations.Commands;
using Market.Domain.ProductComments;
using Market.Domain.ProductComments.Events;
using MediatR;

namespace Market.Application.ProductComment.Commands.CreateProductComment;
public class CreateProductCommentCommandHandler : ICommandHandler<CreateProductCommentCommand, Guid>
{
    private readonly IProductCommentRepository productCommentRepository;
    private readonly IMediator mediator;

    public CreateProductCommentCommandHandler(
        IProductCommentRepository productCommentRepository, IMediator mediator)
    {
        this.productCommentRepository = productCommentRepository;
        this.mediator = mediator;
    }

    public async Task<Guid> Handle(CreateProductCommentCommand request, CancellationToken cancellationToken)
    {
        ProductCommentAggregate productComment =
        new(request.ProductCommentId, request.ProductId, request.UserId, request.Comment, request.Star);

        await productCommentRepository.CreateProductCommentAsync(productComment);

        await mediator.Send(new ProductCommentCreatedByUserDomainEvent(productComment), cancellationToken);
        return productComment.ProductCommentId.Id;
    }
}
