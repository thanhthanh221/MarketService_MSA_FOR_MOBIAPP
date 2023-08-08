using Market.Domain.Core;
using Market.Domain.ProductComments.Events;
using Market.Domain.ProductComments.Exceptions;

namespace Market.Domain.ProductComments;
public class ProductCommentAggregate : Entity, IAggregateRoot
{
    public ProductCommentId ProductCommentId { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid UserCommentId { get; private set; }
    public string Comment { get; private set; }
    public int Star { get; private set; }
    public DateTime TimeComment { get; private set; }
    public bool CheckUserEditComment { get; private set; }
    public DateTime? TimeEdit { get; private set; }

    public ProductCommentAggregate(
        ProductCommentId productCommentId, Guid productId,
        Guid userCommentId, string comment, int star)
    {
        // Check Rules Aggregate Root
        if (string.IsNullOrWhiteSpace(comment)) throw new CommentValueIsNull();
        if (star > 5 || star < 0) throw new StarValueNotValidate();

        // Value Aggreagte Root
        ProductCommentId = productCommentId;
        ProductId = productId;
        UserCommentId = userCommentId;
        Comment = comment;
        Star = star;
        TimeComment = DateTime.UtcNow;
        CheckUserEditComment = false;

        AddDomainEvent(new ProductCommentCreatedByUserDomainEvent(this));
    }

    public void UserEditedComment(string newComment) {
        TimeEdit = DateTime.UtcNow;
        Comment = newComment;
        CheckUserEditComment = true;
        AddDomainEvent(new ProductCommentUserEditCommentDomainEvent(ProductCommentId, newComment));  
    }
}
