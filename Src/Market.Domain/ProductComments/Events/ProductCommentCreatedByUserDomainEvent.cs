using Market.Domain.Core;

namespace Market.Domain.ProductComments.Events;
public class ProductCommentCreatedByUserDomainEvent : DomainEventBase
{
    public ProductCommentAggregate ProductComment { get; private set; }
    public ProductCommentCreatedByUserDomainEvent(ProductCommentAggregate productComment)
    {
        ProductComment = productComment;
    }
}
