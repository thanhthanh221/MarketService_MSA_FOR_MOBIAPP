namespace Market.Domain.ProductComments;
public class ProductCommentSnapshot
{
    public Guid ProductCommentId { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserCommentId { get; set; }
    public string Comment { get; set; }
    public int Star { get; set; }
    public DateTime TimeComment { get; set; }
    public bool CheckUserEditComment { get; set; }
    public DateTime? TimeEdit { get; set; }
    
    public static ProductCommentSnapshot ConvertAggregateToSnapshot(ProductCommentAggregate productComment){
        return new() {
            ProductCommentId = productComment.ProductCommentId.Id,
            ProductId = productComment.ProductId,
            UserCommentId = productComment.UserCommentId,
            Comment = productComment.Comment,
            Star = productComment.Star,
            TimeComment = productComment.TimeComment,
            CheckUserEditComment = productComment.CheckUserEditComment,
            TimeEdit = productComment.TimeEdit,
        };
    }
}
