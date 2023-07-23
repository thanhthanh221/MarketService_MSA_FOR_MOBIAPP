using Market.Domain.Core;

namespace Market.Domain.ProductComments.Events;
public class ProductCommentUserEditCommentDomainEvent : DomainEventBase
{
    public ProductCommentId ProductCommentId { get; private set; }
    public string NewComment { get; private set; }

    public ProductCommentUserEditCommentDomainEvent(
        ProductCommentId productCommentId, string newComment)
    {
        ProductCommentId = productCommentId;
        NewComment = newComment;
    }
}
