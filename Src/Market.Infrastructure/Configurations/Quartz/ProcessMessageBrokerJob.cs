using Market.Application.Common.OutBox;
using Quartz;

namespace Market.Infrastructure.Configurations.Quartz;
public class ProcessMessageBrokerJob : IJob
{
    private readonly IOutBox outBox;

    public ProcessMessageBrokerJob(IOutBox outBox)
    {
        this.outBox = outBox;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await outBox.ProcessOutbox();
    }
}
