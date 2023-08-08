using Market.Application.Coupons.Commands.CreateCoupon;

namespace Api.Aggregates.Coupons.Requests;
public class CreateCouponRequest
{
    public Guid CouponId { get; set; }
    public string Code { get; set; }
    public string Titile { get; set; }
    public List<string> Descriptios { get; set; }
    public decimal PriceMinOrder { get; set; }
    public decimal PriceReduced { get; set; }
    public int Amount { get; set; }
    public Guid AdminId { get; set; }

    public CreateCouponCommand ConvertRequestToCommand()
    {
        return new(
            CouponId, Code, Titile, Descriptios, PriceMinOrder, 
            PriceReduced, Amount, AdminId, DateTime.UtcNow);
    }

}
