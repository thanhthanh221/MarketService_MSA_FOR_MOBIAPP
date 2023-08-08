using Market.Domain.Coupons.Exceptions;
using Market.Domain.Users;

namespace Market.Domain.Coupons;
public class CouponInfomation
{
    public string Code { get; private set; }
    public string Titile { get; private set; }
    public List<string> Descriptios { get; private set; }
    public decimal PriceMinOrder { get; private set; }
    public decimal PriceReduced { get; private set; }
    public int Amount { get; private set; }
    public int CountCouponUse { get; private set; }
    public UserId AdminId { get; private set; }
    public DateTime Expired { get; private set; }

    public CouponInfomation(
        string code,
        string titile,
        List<string> descriptios,
        decimal priceMinOrder,
        decimal priceReduced,
        int amount,
        UserId adminId,
        DateTime expired)
    {
        if (amount < 0) throw new AmountIsValidate();
        if (priceReduced < 0) throw new PriceReducedNotValidate();
        if (priceMinOrder < 0) throw new PriceMinNotValidate();


        Code = code;
        Titile = titile;
        Descriptios = descriptios;
        PriceMinOrder = priceMinOrder;
        PriceReduced = priceReduced;
        Amount = amount;
        AdminId = adminId;
        CountCouponUse = 0;
        Expired = expired;
    }

    public void SetCouponAmount(int newAmount)
    {
        if (newAmount < 0) throw new AmountIsValidate();
        Amount = newAmount;
    }
    public void SetCountCouponUse(int newCount)
    {
        if (newCount < 0) throw new AmountIsValidate();
        CountCouponUse = newCount;
    }
}
