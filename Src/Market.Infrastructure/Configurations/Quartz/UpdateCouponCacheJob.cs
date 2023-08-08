using Market.Application.Common.Cache;
using Market.Domain.Coupons;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Market.Infrastructure.Configurations.Quartz;
public class UpdateCouponCacheJob : IJob
{
    private readonly ICouponRepository couponRepository;
    private readonly ILogger<UpdateCouponCacheJob> logger;
    private readonly IReposeCache reposeCache;

    public UpdateCouponCacheJob(ICouponRepository couponRepository, ILogger<UpdateCouponCacheJob> logger, IReposeCache reposeCache)
    {
        this.couponRepository = couponRepository;
        this.logger = logger;
        this.reposeCache = reposeCache;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine(1);
    }
}
