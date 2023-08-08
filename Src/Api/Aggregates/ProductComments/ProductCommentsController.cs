using Microsoft.AspNetCore.Mvc;
using Market.Application.Common.Bus;
using Api.Aggregates.ProductComments.Requests;
using Market.Application.ProductComment.Commands.CreateProductComment;
using Market.Application.ProductComment.Queries.GetProductCommentByProductIdPagging;

namespace Api.Aggregates.ProductComments;
[ApiController]
[Route("Market/[controller]")]
public class ProductCommentsController : ControllerBase
{
    private readonly IMessageBus messageBus;

    public ProductCommentsController(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
    }
    [HttpGet]
    [Route("GetProductCommentByProductIdPagging")]
    public async Task<ActionResult> GetProductCommentByProductIdPaggingAsync([FromQuery] GetProductCommentByProductIdPaggingRequest request)
    {
        try
        {
            GetProductCommentByProductIdPaggingQuery query =
                new(new(request.ProductId), request.Page, request.PageSize);
            var response = await messageBus.Send(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPost]
    [Route("CreateProductComment")]
    public async Task<ActionResult> CreateProductCommentAsync(CreateProductCommentRequest request)
    {
        try
        {
            CreateProductCommentCommand command =
                new(Guid.NewGuid(), request.ProductId, request.UserId, request.Comment, request.Star);

            var productCommentId = await messageBus.Send(command);
            return Ok(productCommentId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
