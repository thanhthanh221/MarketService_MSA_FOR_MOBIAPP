using Market.Application.Contracts;

namespace Market.Application.Coupons.Commands.CreateCoupon;
public class CreateCouponCommand : CommandBase<Guid>
{
    public Guid CouponId { get; private set; }
    public string Code { get; private set; }
    public string Titile { get; private set; }
    public List<string> Descriptios { get; private set; }
    public decimal PriceMinOrder { get; private set; }
    public decimal PriceReduced { get; private set; }
    public int Amount { get; private set; }
    public Guid AdminId { get; private set; }
    public DateTime Expired { get; private set; }

    public CreateCouponCommand(Guid couponId,
                               string code,
                               string titile,
                               List<string> descriptios,
                               decimal priceMinOrder,
                               decimal priceReduced,
                               int amount,
                               Guid adminId,
                               DateTime expired)
    {
        CouponId = couponId;
        Code = code;
        Titile = titile;
        Descriptios = descriptios;
        PriceMinOrder = priceMinOrder;
        PriceReduced = priceReduced;
        Amount = amount;
        AdminId = adminId;
        Expired = expired;
    }
}
