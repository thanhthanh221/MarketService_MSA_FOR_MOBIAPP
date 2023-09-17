using Market.Infrastructure.Configurations.Quartz;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Market.Infrastructure.Dependency;
public class QuartzDependency : IInstaller
{
    public void Installer(IServiceCollection services, IConfiguration configuration)
    {
        // Đăng ký công việc của bạn (MyJob)
        services.AddScoped<UpdateProductsCacheJob>();
        services.AddScoped<UpdateProductCommentCacheJob>();
        services.AddScoped<UpdateCouponCacheJob>();

        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            var jobUpdateProductKey = JobKey.Create(nameof(UpdateProductsCacheJob));
            q.AddJob<UpdateProductsCacheJob>(jobUpdateProductKey)
            .AddTrigger(t => t.ForJob(jobUpdateProductKey).WithSimpleSchedule(s =>
                s.WithIntervalInHours(24).RepeatForever()));

            var jobUpdateProductCommetKey = JobKey.Create(nameof(UpdateProductCommentCacheJob));
            q.AddJob<UpdateProductCommentCacheJob>(jobUpdateProductCommetKey)
            .AddTrigger(t => t.ForJob(jobUpdateProductCommetKey).WithSimpleSchedule(s =>
                s.WithIntervalInHours(24).RepeatForever()));

            var jobUpdateCouponKey = JobKey.Create(nameof(UpdateCouponCacheJob));
            q.AddJob<UpdateCouponCacheJob>(jobUpdateCouponKey)
            .AddTrigger(t => t.ForJob(jobUpdateCouponKey).WithSimpleSchedule(s =>
                s.WithIntervalInHours(24).RepeatForever()));

            var jobProcessMessageBroker = JobKey.Create(nameof(ProcessMessageBrokerJob));
            q.AddJob<ProcessMessageBrokerJob>(jobProcessMessageBroker)
            .AddTrigger(t => t.ForJob(jobProcessMessageBroker).WithSimpleSchedule(s =>
                s.WithIntervalInMinutes(1).RepeatForever()));
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}
