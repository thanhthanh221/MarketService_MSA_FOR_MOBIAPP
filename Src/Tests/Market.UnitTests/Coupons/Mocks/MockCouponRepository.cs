using Market.Domain.Coupons;
using Market.Domain.Users;
using Moq;

namespace Market.UnitTests.Coupons.Mocks;
public static class MockCouponRepository
{
    public static Mock<ICouponRepository> CreateCouponRepository()
    {
        var mockRepoCoupon = new Mock<ICouponRepository>();

        mockRepoCoupon.Setup(c => c.CreateCouponAsync(It.IsAny<CouponAggregate>()))
            .Returns(Task.FromResult(NewCoupon()));

        return mockRepoCoupon;
    }
    public static CouponAggregate NewCoupon()
    {
        CouponId couponId = new(Guid.NewGuid());
        UserId adminId = new(Guid.NewGuid());
        List<string> descriptios = new(){
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
        };
        CouponInfomation couponInfomation = new(
            Guid.NewGuid().ToString()[..5], Guid.NewGuid().ToString()[..7],
            descriptios, 60000, 10000, 64, adminId,
            new DateTime(2023, 9, 11, 12, 30, 0)
        );
        return new(couponId, couponInfomation);
    }

}
