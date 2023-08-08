using System.Text.Json;
using EventStore.Client;
using Market.Domain.Coupons;
namespace Market.Infrastructure.EventSouring.Coupons;
public class CouponEventStore : ICouponEventStore
{
    private readonly EventStoreClient eventStoreClient;
    public CouponEventStore(EventStoreClient eventStoreClient)
    {
        this.eventStoreClient = eventStoreClient;
    }
    private static string GetCouponStreamName(CouponId couponId)
    {
        return $"Coupon-{couponId.Id}";
    }
    public async Task<CouponAggregate> GetDomainEventAsync(CouponId couponId, CancellationToken cancellationToken = default)
    {
        eventStoreClient.ReadStreamAsync(
            Direction.Forwards,
            GetCouponStreamName(couponId),
            StreamPosition.Start,
            cancellationToken: cancellationToken
        );
        return null;
    }

    public async Task SaveDomainEventAsync(CouponAggregate couponAggregate)
    {
        if (!couponAggregate.DomainEvents.Any()) return;

        var changes = couponAggregate.DomainEvents
            .Select(change => new EventData(
                eventId: Uuid.NewUuid(),
                type: change.GetType().Name,
                data: JsonSerializer.SerializeToUtf8Bytes(change)));

        await eventStoreClient.AppendToStreamAsync(
            GetCouponStreamName(couponAggregate.CouponId),
            StreamRevision.FromInt64(couponAggregate.Version),
            changes
        );
    }

    public async Task SaveDomainEventAsync(CouponId couponId,
        object domainEvent, CancellationToken cancellationToken = default)
    {
        var eventData = new EventData(
            eventId: Uuid.NewUuid(),
            type: domainEvent.GetType().Name,
            data: JsonSerializer.SerializeToUtf8Bytes(domainEvent)
        );
        await eventStoreClient.AppendToStreamAsync(
            GetCouponStreamName(couponId),
            StreamRevision.FromInt64(-1),
            new List<EventData>() { eventData },
            cancellationToken: cancellationToken
        );
    }
}
