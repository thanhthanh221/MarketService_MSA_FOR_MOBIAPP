using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using Market.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Market.Application.Coupons.Commands.RollbackCoupon;
public class RollbackCouponCommandHandler : ICommandHandler<RollbackCouponCommand, bool>
{
    private readonly ICouponRepository couponRepository;
    private readonly ILogger<RollbackCouponCommandHandler> logger;
    private readonly IMessageBus messageBus;

    public RollbackCouponCommandHandler(
        ICouponRepository couponRepository, ILogger<RollbackCouponCommandHandler> logger, IMessageBus messageBus)
    {
        this.couponRepository = couponRepository;
        this.logger = logger;
        this.messageBus = messageBus;
    }

    public async Task<bool> Handle(RollbackCouponCommand request, CancellationToken cancellationToken)
    {
        CouponId couponId = new(request.CouponId);

        UserId userId = new(request.UserId);

        var coupon = await couponRepository.GetCouponByIdAsync(couponId);

        if (coupon is null) return false;

        coupon.UserRecoveredCoupon(userId);
        await couponRepository.UpdateCouponAsync(coupon);

        logger.LogInformation($"RollBack Coupon: {couponId}");
        await messageBus.Publish(new CouponRecoveredDomainEvent(
            couponId, userId, coupon.CouponInfomation.CountCouponUse), cancellationToken);
        return true;
    }
}
