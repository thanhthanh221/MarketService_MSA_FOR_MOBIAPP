namespace Market.Domain.Coupons;
public interface ICouponRepository
{
    Task CreateCouponAsync(CouponAggregate couponAggregate);
    Task<CouponAggregate> GetCouponByIdAsync(CouponId couponId);
    Task<List<CouponAggregate>> GetAllCouponAsync();
    Task UpdateCouponAsync(CouponAggregate couponAggregate);
    Task RemoveCouponAsync(CouponId couponId);
}