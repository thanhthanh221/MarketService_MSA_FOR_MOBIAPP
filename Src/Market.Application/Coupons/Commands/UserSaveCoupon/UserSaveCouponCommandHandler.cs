using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using Market.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Market.Application.Coupons.Commands.UserSaveCoupon;
public class UserSaveCouponCommandHandler : ICommandHandler<UserSaveCouponCommand, Guid>
{
    private readonly ICouponRepository couponRepository;
    private readonly IMessageBus messageBus;
    private readonly ILogger<UserSaveCouponCommandHandler> logger;

    public UserSaveCouponCommandHandler(
        ICouponRepository couponRepository, IMessageBus messageBus, ILogger<UserSaveCouponCommandHandler> logger)
    {
        this.couponRepository = couponRepository;
        this.messageBus = messageBus;
        this.logger = logger;
    }

    public async Task<Guid> Handle(UserSaveCouponCommand request, CancellationToken cancellationToken)
    {
        CouponId couponId = new(request.CouponId);
        UserId userId = new(request.UserId);

        var coupon = await couponRepository.GetCouponByIdAsync(couponId);
        coupon.UserSaveCoupon(userId);

        await couponRepository.UpdateCouponAsync(coupon);

        logger.LogInformation($"User: {userId} save coupon: {couponId}");

        await messageBus.Publish(new CouponSavedByUserDomainEvent(
            userId, couponId, coupon.CouponInfomation.Amount, coupon.CouponUsers.Count), cancellationToken);
        return couponId.Id;
    }
}
