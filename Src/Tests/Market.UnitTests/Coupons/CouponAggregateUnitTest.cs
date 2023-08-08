using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using Market.Domain.Users;

namespace Market.UnitTests.Coupons;
public class CouponAggregateUnitTest
{
    private readonly CouponAggregate couponAggregate;
    public CouponAggregateUnitTest()
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
            Guid.NewGuid().ToString()[..5],Guid.NewGuid().ToString()[..7],
            descriptios,60000,10000,64,adminId,
            new DateTime(2023,9,11,12,30,0)
        );

        couponAggregate = new(couponId,couponInfomation);
    }

    [Fact]
    public void CreateCoupon_IsSuccessful()
    {
        Assert.NotNull(couponAggregate);
        Assert.IsType<CouponCreatedByAdminDomainEvent>(couponAggregate.DomainEvents.First());
    }
}
