using Market.Domain.Core;

namespace Market.Domain.ProductComments;

public class ProductCommentId : TypedIdValueBase
{
    public ProductCommentId(Guid value) : base(value)
    {
    }
}
