using Event.Message.CreateOrder;
using Event.Message.CreateOrder.MarketService.Products;
using Market.Application.Common.Bus;
using Market.Application.Common.OutBox;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using MassTransit;
using Newtonsoft.Json;

namespace Market.Application.Products.Consumers;
public class CreatedOrderRollBackProductConsumer : IConsumer<CreatedOrderFailProductServiceEvent>
{
    private readonly IProductRepository productRepository;
    private readonly IMessageBus messageBus;
    private readonly IOutBox outBox;

    private CreatedOrderFailProductServiceEvent RollBackEvent { get; set; }
    private Dictionary<ProductAggregate, ProductDataEvent> RollBackData { get; set; } = new();
    public CreatedOrderRollBackProductConsumer(IProductRepository productRepository,
        IMessageBus messageBus, IOutBox outBox)
    {
        this.productRepository = productRepository;
        this.messageBus = messageBus;
        this.outBox = outBox;
    }
    public CreatedOrderRollBackProductConsumer()
    {
    }
    public async Task Consume(ConsumeContext<CreatedOrderFailProductServiceEvent> context)
    {
        RollBackEvent = new(context.Message);

        foreach (var o in context.Message.OrderItems)
        {
            ProductId productId = new(o.OrderItemId);
            ProductTypeValueId productTypeValueId = new(o.OrderItemTypeId);

            var product = await productRepository.GetProductByIdAsync(productId);

            ProductTypeValue productType = product.ProductType
                .GetProductTypeByProductTypeId(productTypeValueId);

            product.BuyFailProductProduct(new(context.Message.CustomerId),
                productTypeValueId, o.Quantity);
            RollBackData.Add(product, o);
        }

        foreach (var i in RollBackData)
        {
            await productRepository.UpdateProductAsync(i.Key);

            ProductTypeBoughtEvent orderItem = new(
                new(i.Value.OrderItemTypeId), i.Value.Price, i.Value.Quantity);

            await messageBus.Publish(new RollBackBoughtProductDomainEvent(
                new(context.Message.CustomerId), new(i.Value.OrderItemId), orderItem));
        }
        await outBox.AddAsync(new(nameof(CreatedOrderFailProductServiceEvent).ToString(),
            JsonConvert.SerializeObject(RollBackData)));
    }
}
