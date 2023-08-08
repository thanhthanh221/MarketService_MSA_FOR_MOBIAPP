using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using Market.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Market.Application.Coupons.Commands.UserOrderUseCoupon;
public class UserOrderUseCouponCommandHandler : ICommandHandler<UserOrderUseCouponCommand, bool>
{
    private readonly ICouponRepository couponRepository;
    private readonly ILogger<UserOrderUseCouponCommandHandler> logger;
    private readonly IMessageBus messageBus;

    public UserOrderUseCouponCommandHandler(
        ICouponRepository couponRepository, ILogger<UserOrderUseCouponCommandHandler> logger, IMessageBus messageBus)
    {
        this.couponRepository = couponRepository;
        this.logger = logger;
        this.messageBus = messageBus;
    }

    public async Task<bool> Handle(UserOrderUseCouponCommand request,
        CancellationToken cancellationToken)
    {
        CouponId couponId = new(request.CouponId);

        var coupon = await couponRepository.GetCouponByIdAsync(couponId);
        if (coupon is null) return false;

        UserId userId = new(request.UserId);
        coupon.UserUseCoupon(userId);

        await couponRepository.UpdateCouponAsync(coupon);

        logger.LogInformation($"User: {userId.Id} Order use Coupon: {couponId.Id}");
        await messageBus.Publish(new CouponUsedByUserDomainEvent(
            userId, couponId, coupon.CouponInfomation.CountCouponUse), cancellationToken);

        return true;

    }
}
