using Market.Application.Contracts;
using Market.Domain.ProductComments;
using Market.Domain.Products;
using Market.Domain.Users;

namespace Market.Application.ProductComment.Commands.CreateProductComment;

public class CreateProductCommentCommand : CommandBase<Guid>
{
    public ProductCommentId ProductCommentId { get; private set; }
    public ProductId ProductId { get; private set; }
    public UserId UserId { get; private set; }
    public string Comment { get; private set; }
    public int Star { get; private set; }

    public CreateProductCommentCommand(
        ProductCommentId productCommentId,
        ProductId productId,
        UserId userId,
        string comment,
        int star)
    {
        ProductCommentId = productCommentId;
        ProductId = productId;
        UserId = userId;
        Comment = comment;
        Star = star;
    }


}
