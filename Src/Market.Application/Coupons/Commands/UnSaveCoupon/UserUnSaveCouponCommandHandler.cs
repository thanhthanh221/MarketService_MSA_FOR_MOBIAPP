using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using Market.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Market.Application.Coupons.Commands.UserUnSaveCoupon;
public class UserUnSaveCouponCommandHandler : ICommandHandler<UserUnSaveCouponCommand, Guid>
{
    private readonly ICouponRepository couponRepository;
    private readonly IMessageBus messageBus;
    private readonly ILogger<UserUnSaveCouponCommandHandler> logger;

    public UserUnSaveCouponCommandHandler(ICouponRepository couponRepository, IMessageBus messageBus, ILogger<UserUnSaveCouponCommandHandler> logger)
    {
        this.couponRepository = couponRepository;
        this.messageBus = messageBus;
        this.logger = logger;
    }

    public async Task<Guid> Handle(UserUnSaveCouponCommand request, CancellationToken cancellationToken)
    {
        CouponId couponId = new(request.CouponId);
        UserId userId = new(request.UserId);

        var coupon = await couponRepository.GetCouponByIdAsync(couponId);

        if (coupon is null) return Guid.Empty;

        coupon.UnSaveCoupon(userId);
        await couponRepository.UpdateCouponAsync(coupon);

        await messageBus.Publish(new CouponUnSavedByUserDomainEvent(
            userId, couponId, coupon.CouponInfomation.Amount, coupon.CouponUsers.Count), cancellationToken);

        logger.LogInformation($"User: {userId} un save coupon: {couponId}");
        return couponId.Id;
    }
}
