using Event.Message.CreateOrder;
using Event.Message.CreateOrder.MarketService.Coupons;
using Market.Application.Common.Bus;
using Market.Application.Common.OutBox;
using Market.Domain.Coupons;
using Market.Domain.Coupons.Events;
using MassTransit;
using Newtonsoft.Json;

namespace Market.Application.Coupons.Consumers;
public class CreatedOrderCouponServiceConsumer : IConsumer<ICreatedOrderMessageEvent>
{
    private readonly ICouponRepository couponRepository;
    private readonly IMessageBus messageBus;
    private readonly IOutBox outBox;

    private List<CouponAggregate> CouponsUse { get; set; } = new();
    private CreatedOrderCouponServiceEvent SuscessEvent { get; set; }
    private CreatedOrderFailCouponServiceEvent FailEvent { get; set; }

    public CreatedOrderCouponServiceConsumer(ICouponRepository couponRepository,
        IMessageBus messageBus, IOutBox outBox)
    {
        this.couponRepository = couponRepository;
        this.messageBus = messageBus;
        this.outBox = outBox;
    }

    public CreatedOrderCouponServiceConsumer()
    {
    }

    public async Task Consume(ConsumeContext<ICreatedOrderMessageEvent> context)
    {
        SuscessEvent = new(context.Message);
        FailEvent = new(context.Message);

        if (context.Message.CouponsId.Count == 0)
        {
            await outBox.AddAsync(new(nameof(CreatedOrderCouponServiceEvent).ToString(),
                JsonConvert.SerializeObject(SuscessEvent)));
            return;
        }
        foreach (var c in context.Message.CouponsId)
        {
            CouponId couponId = new(c);
            var coupon = await couponRepository.GetCouponByIdAsync(couponId);

            if (coupon is null || coupon.CouponInfomation.Expired < DateTime.Now)
            {
                context.Message.CheckWorkflow = false;
                break;
            }
            CouponsUse.Add(coupon);
        }

        if (!context.Message.CheckWorkflow)
        {
            await outBox.AddAsync(new(nameof(CreatedOrderFailCouponServiceEvent).ToString(),
                JsonConvert.SerializeObject(FailEvent)));
        }
        else
        {
            foreach (var c in CouponsUse)
            {
                c.UseCoupon(new(context.Message.CustomerId));

                await couponRepository.UpdateCouponAsync(c);

                await messageBus.Publish(new UsedCouponDomainEvent(
                    new(context.Message.CustomerId), c.CouponId, c.CouponInfomation.Amount));
            }

            await outBox.AddAsync(new(nameof(CreatedOrderCouponServiceEvent).ToString(),
                JsonConvert.SerializeObject(SuscessEvent)));
        }
    }
}
