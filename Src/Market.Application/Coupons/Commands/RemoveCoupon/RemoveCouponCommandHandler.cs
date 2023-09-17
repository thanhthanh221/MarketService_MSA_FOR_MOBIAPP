using Market.Application.Configurations.Commands;
using Market.Domain.Coupons;
using Market.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Market.Application.Coupons.Commands.RemoveCoupon;
public class RemoveCouponCommandHandler : ICommandHandler<RemoveCouponCommand, Guid>
{
    private readonly ICouponRepository couponRepository;
    private readonly ILogger<RemoveCouponCommandHandler> logger;

    public RemoveCouponCommandHandler(
        ICouponRepository couponRepository, ILogger<RemoveCouponCommandHandler> logger)
    {
        this.couponRepository = couponRepository;
        this.logger = logger;
    }

    public async Task<Guid> Handle(RemoveCouponCommand request, CancellationToken cancellationToken)
    {
        CouponId couponId = new(request.CouponId);
        UserId adminId = new(request.AdminId);

        var coupon = await couponRepository.GetCouponByIdAsync(couponId);

        coupon.RemoveCoupon(adminId);
        await couponRepository.UpdateCouponAsync(coupon);

        logger.LogInformation($"Admin {adminId.Id} remove {couponId.Id}");

        return couponId.Id;
    }
}
