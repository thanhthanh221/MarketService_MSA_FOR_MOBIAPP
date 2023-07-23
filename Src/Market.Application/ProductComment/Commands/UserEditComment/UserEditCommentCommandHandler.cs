using Market.Application.Configurations.Commands;
using Market.Domain.ProductComments;
using Microsoft.Extensions.Logging;

namespace Market.Application.ProductComment.Commands.UserEditComment;
public class UserEditCommentCommandHandler : ICommandHandler<UserEditCommentCommand, Guid>
{
    private readonly IProductCommentRepository productCommentRepository;
    private readonly ILogger<UserEditCommentCommandHandler> logger;

    public UserEditCommentCommandHandler(
        IProductCommentRepository productCommentRepository, ILogger<UserEditCommentCommandHandler> logger)
    {
        this.productCommentRepository = productCommentRepository;
        this.logger = logger;
    }

    public async Task<Guid> Handle(UserEditCommentCommand request, CancellationToken cancellationToken)
    {
        var productComment = await productCommentRepository
            .GetProductCommentByIdAsync(request.ProductCommentId);

        productComment.UserEditedComment(request.NewComment);

        logger.LogInformation(
            $"User: {productComment.UserCommentId} Edit Comment: {productComment.ProductCommentId} In Product: {productComment.ProductId}");
        return productComment.ProductCommentId.Id;
    }
}
