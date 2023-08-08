using System.ComponentModel.DataAnnotations;

namespace Api.Aggregates.ProductComments.Requests;
public class GetProductCommentByProductIdPaggingRequest
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Page { get; set; }
    [Required]
    [Range(5, int.MaxValue)]
    public int PageSize { get; set; }
    [Required]
    public Guid ProductId { get; set; }
}
