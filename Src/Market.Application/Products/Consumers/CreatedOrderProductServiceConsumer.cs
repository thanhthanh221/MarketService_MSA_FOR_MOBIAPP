using Event.Message.CreateOrder;
using Event.Message.CreateOrder.MarketService.Products;
using Market.Application.Common.Bus;
using Market.Application.Common.OutBox;
using Market.Domain.Products;
using Market.Domain.Products.Events;
using MassTransit;
using Newtonsoft.Json;

namespace Market.Application.Products.Consumers;
public class CreatedOrderProductServiceConsumer : IConsumer<ICreatedOrderMessageEvent>
{
    private readonly IProductRepository productRepository;
    private readonly IMessageBus messageBus;
    private readonly IOutBox outBox;

    private CreatedOrderFailProductServiceEvent FailEvent { get; set; }
    private CreatedOrderProductServiceEvent SuscessEvent { get; set; }

    private Dictionary<ProductAggregate, ProductDataEvent> DataConfig { get; set; } = new();

    public CreatedOrderProductServiceConsumer()
    {
    }

    public CreatedOrderProductServiceConsumer(IProductRepository productRepository,
        IMessageBus messageBus, IOutBox outBox)
    {
        this.productRepository = productRepository;
        this.messageBus = messageBus;
        this.outBox = outBox;
    }

    public async Task Consume(ConsumeContext<ICreatedOrderMessageEvent> context)
    {
        SuscessEvent = new(context.Message);
        FailEvent = new(context.Message);


        foreach (var o in context.Message.OrderItems)
        {
            ProductId productId = new(o.OrderItemId);
            ProductTypeValueId productTypeValueId = new(o.OrderItemTypeId);

            var product = await productRepository.GetProductByIdAsync(productId);

            ProductTypeValue productType = product.ProductType
                .GetProductTypeByProductTypeId(productTypeValueId);

            if (product is null || productType is null || productType.QuantityType < o.Quantity)
            {
                context.Message.CheckWorkflow = false;
                break;
            }
            product.BuyProduct(new(context.Message.CustomerId), productTypeValueId, o.Quantity);
            DataConfig.Add(product, o);
        }

        if (context.Message.CheckWorkflow)
        {
            foreach (var i in DataConfig)
            {
                await productRepository.UpdateProductAsync(i.Key);

                ProductTypeBoughtEvent orderItem = new(
                    new(i.Value.OrderItemTypeId), i.Value.Price, i.Value.Quantity);

                await messageBus.Publish(new BoughtProductDomainEvent(
                    new(context.Message.CustomerId), new(i.Value.OrderItemId), orderItem));
            }
            await outBox.AddAsync(new(nameof(CreatedOrderProductServiceEvent).ToString(),
                JsonConvert.SerializeObject(SuscessEvent)));

            return;
        }

        await outBox.AddAsync(new(nameof(CreatedOrderFailProductServiceEvent).ToString(),
            JsonConvert.SerializeObject(FailEvent)));
    }
}
