using Market.Domain.Users;

namespace Market.Domain.Coupons;
public interface ICouponRepository
{
    Task CreateCouponAsync(CouponAggregate couponAggregate);
    Task<CouponAggregate> GetCouponByIdAsync(CouponId couponId);
    Task<List<CouponAggregate>> GetCouponByUserId(UserId userId);
    Task RemoveCouponAsync(CouponId couponId);
}