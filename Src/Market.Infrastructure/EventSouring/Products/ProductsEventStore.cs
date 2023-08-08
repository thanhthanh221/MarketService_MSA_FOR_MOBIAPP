using System.Text.Json;
using EventStore.Client;
using Market.Domain.Products;

namespace Market.Infrastructure.EventSouring.Products;
public class ProductEventStore : IProductEventStore
{
    private readonly EventStoreClient eventStoreClient;
    public ProductEventStore(EventStoreClient eventStoreClient)
    {
        this.eventStoreClient = eventStoreClient;
    }
    private static string GetProductStreamName(ProductId productId)
    {
        return $"Product-{productId.Id}";
    }

    public Task GetProductDomainEventAsync(ProductId productId)
    {
        throw new NotImplementedException();
    }

    public async Task SaveProductDomainEventAsync(ProductId productId, object domainEvent, CancellationToken cancellationToken = default)
    {
        var eventData = new EventData(
            eventId: Uuid.NewUuid(),
            type: domainEvent.GetType().Name,
            data: JsonSerializer.SerializeToUtf8Bytes(domainEvent)
        );
        await eventStoreClient.AppendToStreamAsync(
            GetProductStreamName(productId),
            StreamState.Any,
            new List<EventData>(){eventData},
            cancellationToken: cancellationToken
        );
    }

    public async Task SaveProductDomainEventAsync(ProductAggregate productAggregate)
    {
        if (!productAggregate.DomainEvents.Any()) return;

        var events = productAggregate.DomainEvents
            .Select(change => new EventData(
                eventId: Uuid.NewUuid(),
                type: change.GetType().Name,
                data: JsonSerializer.SerializeToUtf8Bytes(change)));

        await eventStoreClient.AppendToStreamAsync(
            GetProductStreamName(productAggregate.ProductId),
            StreamState.Any,
            events
        );
    }
    
}
