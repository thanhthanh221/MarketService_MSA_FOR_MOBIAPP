using Event.Message.CreateOrder;
using Event.Message.CreateOrder.MarketService.Coupons;
using Market.Application.Common.OutBox;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using MassTransit;
using MediatR;
using Newtonsoft.Json;

namespace Market.Application.Coupons.Consumers;
public class CreatedOrderRollBackCouponConsumer : IConsumer<ICreatedOrderMessageEvent>
{
    private readonly ICouponRepository couponRepository;
    private readonly IMediator mediator;
    private readonly IOutBox outBox;

    private List<CouponAggregate> CouponsRollBack { get; set; } = new();
    private CreatedOrderFailCouponServiceEvent RollBackEvent { get; set; }

    public CreatedOrderRollBackCouponConsumer()
    {
    }

    public CreatedOrderRollBackCouponConsumer(ICouponRepository couponRepository,
        IMediator mediator, IOutBox outBox)
    {
        this.couponRepository = couponRepository;
        this.mediator = mediator;
        this.outBox = outBox;
    }

    public async Task Consume(ConsumeContext<ICreatedOrderMessageEvent> context)
    {
        RollBackEvent = new(context.Message);

        if (context.Message.CouponsId.Count == 0)
        {
            await outBox.AddAsync(new(nameof(CreatedOrderFailCouponServiceEvent).ToString(),
                JsonConvert.SerializeObject(RollBackEvent)));
            return;
        }

        foreach (var c in context.Message.CouponsId)
        {
            CouponId couponId = new(c);
            var coupon = await couponRepository.GetCouponByIdAsync(couponId);

            CouponsRollBack.Add(coupon);
        }

        foreach (var c in CouponsRollBack)
        {
            c.UseCouponFail(new(context.Message.CustomerId));

            await couponRepository.UpdateCouponAsync(c);

            await mediator.Publish(new RecoveredCouponDomainEvent(
                c.CouponId, new(context.Message.CustomerId), c.CouponInfomation.Amount));
        }

        await outBox.AddAsync(new(nameof(CreatedOrderFailCouponServiceEvent).ToString(),
            JsonConvert.SerializeObject(RollBackEvent)));
    }
}
