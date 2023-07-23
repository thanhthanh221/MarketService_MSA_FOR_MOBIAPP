using Market.Application.Contracts;
using Market.Domain.ProductComments;
using Market.Domain.Users;

namespace Market.Application.ProductComment.Commands.UserEditComment;
public class UserEditCommentCommand : CommandBase<Guid>
{
    public UserId UserId { get; private set; }
    public ProductCommentId ProductCommentId { get; private set; }
    public string NewComment { get; private set; }
}
