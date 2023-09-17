namespace Market.Application.Common.OutBox;
public interface IOutBox
{
    Task AddAsync(OutboxMessage message);
    Task ProcessOutbox(CancellationToken cancellationToken = default);
}
