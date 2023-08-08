using Market.Application.Common.Bus;
using Market.Application.Configurations.Commands;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using Market.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Market.Application.Coupons.Commands.CreateCoupon;
public class CreateCouponCommandHandler : ICommandHandler<CreateCouponCommand, Guid>
{
    private readonly ICouponRepository couponRepository;
    private readonly ILogger<CreateCouponCommandHandler> logger;
    private readonly IMessageBus messageBus;

    public CreateCouponCommandHandler(ICouponRepository couponRepository, ILogger<CreateCouponCommandHandler> logger, IMessageBus messageBus)
    {
        this.couponRepository = couponRepository;
        this.logger = logger;
        this.messageBus = messageBus;
    }

    public async Task<Guid> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        CouponId couponId = new(request.CouponId);
        UserId adminId = new(request.AdminId);
        CouponInfomation couponInfomation = new(request.Code, request.Titile, request.Descriptios,
            request.PriceMinOrder, request.PriceReduced,
            request.Amount, adminId, request.Expired);

        CouponAggregate couponAggregate = new(couponId, couponInfomation);

        await couponRepository.CreateCouponAsync(couponAggregate);

        logger.LogInformation($"Create new coupon with CouponId: {couponId.Id} By Admin: {adminId.Id}");

        await messageBus.Publish(new CouponCreatedByAdminDomainEvent(couponAggregate), cancellationToken);
        return couponId.Id;
    }
}
