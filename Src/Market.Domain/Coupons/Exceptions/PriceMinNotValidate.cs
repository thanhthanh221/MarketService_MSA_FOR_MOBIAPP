namespace Market.Domain.Coupons.Exceptions;

public class PriceMinNotValidate : Exception
{
    public PriceMinNotValidate() : base("Min Price Order is not validate")
    {
    }
}
