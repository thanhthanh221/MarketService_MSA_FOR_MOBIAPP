using Market.Domain.ProductComments;
using Market.Domain.Users;

namespace Market.Application.ProductComment.Queries.AggregateDto;
public class ProductCommentsDto
{
    public Guid ProductCommentId { get; private set; }
    public UserAggregate UserInfomation { get; private set; }
    public string Comment { get; private set; }
    public int Star { get; private set; }
    public DateTime TimeComment { get; private set; }
    public bool CheckUserEditComment { get; private set; }
    public ProductCommentsDto(
        Guid productCommentId, UserAggregate userInfomation, string comment, int star, DateTime timeComment, bool checkUserEditComment)
    {
        ProductCommentId = productCommentId;
        UserInfomation = userInfomation;
        Comment = comment;
        Star = star;
        TimeComment = timeComment;
        CheckUserEditComment = checkUserEditComment;
    }

    public ProductCommentsDto()
    {
    }

    public static ProductCommentsDto ConverProductCommentsToDto(ProductCommentAggregate productComments, UserAggregate user)
    {
        return new() {
            ProductCommentId = productComments.ProductCommentId.Id,
            UserInfomation = user,
            Comment = productComments.Comment,
            Star = productComments.Star,
            TimeComment = productComments.TimeComment,
            CheckUserEditComment = productComments.CheckUserEditComment
        };
    }
}
