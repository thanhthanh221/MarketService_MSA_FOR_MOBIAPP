using Event.Message.CreateOrder;
using Event.Message.CreateOrder.MarketService.Coupons;
using Event.Message.CreateOrder.MarketService.Products;
using Market.Application.Common.OutBox;
using Market.Infrastructure.MarketContext;
using MassTransit;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Market.Infrastructure.Configurations.OutBox;
public class OutboxAccessor : IOutBox
{
    private readonly MarketDbContext context;
    private readonly IPublishEndpoint publishEndpoint;
    private readonly FilterDefinitionBuilder<OutboxMessage> filterBuilder = Builders<OutboxMessage>.Filter;

    public OutboxAccessor(MarketDbContext context, IPublishEndpoint publishEndpoint)
    {
        this.context = context;
        this.publishEndpoint = publishEndpoint;
    }

    public async Task AddAsync(OutboxMessage message)
    {
        await context.OutBox.InsertOneAsync(message);
    }

    public async Task ProcessOutbox(CancellationToken cancellationToken = default)
    {
        var allMessageProcess = await context.OutBox.Find(new BsonDocument()).ToListAsync(cancellationToken);

        var messagesNonProcess = allMessageProcess.Where(m => m.StatusProcess == StatusProcess.NonProcess);
        foreach (var m in messagesNonProcess)
        {
            if (m.Type == "CreatedOrderCouponServiceEvent")
            {
                CreatedOrderCouponServiceEvent @event =
                    JsonConvert.DeserializeObject<CreatedOrderCouponServiceEvent>(m.Data);
                await publishEndpoint.Publish<ICreatedOrderMessageEvent>(@event, cancellationToken);
            }
            else if (m.Type == "CreatedOrderFailCouponServiceEvent")
            {
                CreatedOrderFailCouponServiceEvent @event =
                    JsonConvert.DeserializeObject<CreatedOrderFailCouponServiceEvent>(m.Data);
                await publishEndpoint.Publish<ICreatedOrderMessageEvent>(@event, cancellationToken);
            }
            else if (m.Type == "CreatedOrderProductServiceEvent")
            {
                CreatedOrderProductServiceEvent @event =
                    JsonConvert.DeserializeObject<CreatedOrderProductServiceEvent>(m.Data);
                await publishEndpoint.Publish<ICreatedOrderMessageEvent>(@event, cancellationToken);
            }
            else if (m.Type == "CreatedOrderFailProductServiceEvent")
            {
                CreatedOrderFailProductServiceEvent @event =
                    JsonConvert.DeserializeObject<CreatedOrderFailProductServiceEvent>(m.Data);
                await publishEndpoint.Publish<ICreatedOrderMessageEvent>(@event, cancellationToken);
            }
            m.StatusProcess = StatusProcess.Processed;

            var filter = filterBuilder?.Eq(me => me.Id, m.Id);
            await context.OutBox.ReplaceOneAsync(filter, m, cancellationToken: cancellationToken);
        }
    }
}
