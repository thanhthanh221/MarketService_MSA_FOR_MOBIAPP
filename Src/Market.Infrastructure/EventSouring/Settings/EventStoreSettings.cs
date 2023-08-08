namespace Market.Infrastructure.EventSouring.Settings;
public class EventStoreSettings
{
    public string Host { get; init; }
    public string Port { get; init; }
    public bool Tls { get; init; }
    public string ConnectionString
    {
        get
        {
            return $"esdb://{Host}:{Port}?tls={Tls}";
        }
    }

}
