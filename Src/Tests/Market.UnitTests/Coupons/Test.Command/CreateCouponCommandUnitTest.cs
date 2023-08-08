using Market.Application.Coupons.Commands.CreateCoupon;
using Market.Infrastructure.Configurations.Bus;
using Market.UnitTests.Coupons.Mocks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace Market.UnitTests.Coupons.Test.Command;
public class CreateProductCommandUnitTest
{

    [Fact]
    public async Task CreateCouponAsync_InCreateCouponCommand_ReturnCoupon()
    {
        var loggerMock = new Mock<ILogger<CreateCouponCommandHandler>>();
        var couponRepoMock = MockCouponRepository.CreateCouponRepository();

        var mediatorMock = new Mock<IMediator>();
        var eventBus = new MessageBus(mediatorMock.Object);

        var handler = new CreateCouponCommandHandler(couponRepoMock.Object, loggerMock.Object, eventBus);

        CreateCouponCommand createCouponCommand =
            new(Guid.NewGuid(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
            new List<string>(), 60000, 1000, 56, Guid.NewGuid(), new DateTime(2023, 9, 11));
        var result = await handler.Handle(createCouponCommand, CancellationToken.None);

        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);
    }
}



