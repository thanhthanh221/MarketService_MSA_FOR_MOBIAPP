namespace Market.Domain.ProductComments.Exceptions;

public class CommentValueIsNull : Exception
{
    public CommentValueIsNull() : base("Comment Is Null")
    {
    }
}
