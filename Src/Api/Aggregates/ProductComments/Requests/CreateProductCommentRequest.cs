namespace Api.Aggregates.ProductComments.Requests;
public class CreateProductCommentRequest
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string Comment { get; set; }
    public int Star { get; set; }

}
