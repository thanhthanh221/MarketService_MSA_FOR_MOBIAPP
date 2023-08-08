using Api.Aggregates.Coupons.Requests;
using Market.Application.Common.Bus;
using Market.Application.Coupons.Queries.GetCouponById;
using Microsoft.AspNetCore.Mvc;

namespace Api.Aggregates.Coupons;
[ApiController]
[Route("Market/[controller]")]
public class CouponController : ControllerBase
{
    private readonly IMessageBus messageBus;
    public CouponController(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
    }
    [HttpPost]
    [Route("CreateCoupon")]
    public async Task<ActionResult> CreateProductAsync([FromForm] CreateCouponRequest request)
    {
        try
        {
            var command = request.ConvertRequestToCommand();
            var couponId = await messageBus.Send(command);
            return Ok(couponId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetCouponById")]
    public async Task<ActionResult> GetProductByIdAsync(Guid couponId)
    {
        try
        {
            var couponDto = await messageBus.Send(new GetCouponByIdQuery(new(couponId)));
            return Ok(couponDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
