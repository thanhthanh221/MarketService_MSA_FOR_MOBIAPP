using Market.Api.Aggregates.Product.Requests;
using Market.Application.Common.Bus;
using Market.Application.Common.File;
using Market.Application.Products.Commands.FavouriteProduct;
using Market.Application.Products.Commands.RemoveProduct;
using Market.Application.Products.Queries.GetProductById;
using Market.Application.Products.Queries.GetProductPagging;
using Market.Domain.Products;
using Microsoft.AspNetCore.Mvc;

namespace Api.Aggregates.Products;

[ApiController]
[Route("Market/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMessageBus messageBus;
    private readonly IUploadFile uploadFile;

    public ProductController(IMessageBus messageBus, IUploadFile uploadFile)
    {
        this.messageBus = messageBus;
        this.uploadFile = uploadFile;
    }

    [HttpGet]
    [Route("GetProductById")]
    public async Task<ActionResult> GetProductByIdAsync(Guid productIdRequest)
    {
        ProductId productId = new(productIdRequest);
        GetProductByIdQuery query = new(productId, new(Guid.NewGuid()));

        var productDto = await messageBus.Send(query);
        return Ok(productDto);
    }

    [HttpGet]
    [Route("UserGetProductById")]
    public async Task<ActionResult> GetProductByUserFindByProductIdAsync(Guid productIdRequest, Guid userIdRequest)
    {
        try
        {
            GetProductByIdQuery query = new(new(productIdRequest), new(userIdRequest));

            var productDto = await messageBus.Send(query);
            return Ok(productDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet]
    [Route("UserGetProductPagging")]
    public async Task<ActionResult> GetProductPaggingByUserAsync(int Page, int PageSize, Guid userIdRequest)
    {
        try
        {
            GetProductPaggingQuery query = new(new(userIdRequest), PageSize, Page);
            var response = await messageBus.Send(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet]
    [Route("GetProductPagging")]
    public async Task<ActionResult> GetProductPaggingAsync(int Page, int PageSize)
    {
        GetProductPaggingQuery query = new(new(Guid.NewGuid()), PageSize, Page);
        var response = await messageBus.Send(query);
        return Ok(response);
    }
    [HttpGet]
    [Route("GetAllCategoryProduct")]
    public ActionResult GetAllCategoryProduct()
    {
        return Ok(ProductCategory.GetAllCategoryProduct());
    }
    [HttpPost]
    [Route("CreateProduct")]
    public async Task<ActionResult> CreateProductAsync([FromForm] CreateProductRequest createProductRequest)
    {
        try
        {
            var imageProductUrl = await uploadFile
                .UploadImageToImgur(createProductRequest.ProductImageUri);

            var createProductCommand = createProductRequest.ConvertRequestDataToCommand(imageProductUrl);

            var productId = await messageBus.Send(createProductCommand);
            return Ok(productId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPatch]
    [Route("UserFavouriteProduct")]
    public async Task<ActionResult> UserFavouriteProductAsync(Guid userId, Guid productId)
    {
        try
        {
            FavouriteProductCommand command = new(new(productId), new(userId));
            var response = await messageBus.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpDelete]
    [Route("RemoveProduct")]
    public async Task<ActionResult> DeleteProductAsync(Guid productId)
    {
        try
        {
            RemoveProductCommand command = new(new(productId));
            var response = await messageBus.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
