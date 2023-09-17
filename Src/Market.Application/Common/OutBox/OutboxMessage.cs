namespace Market.Application.Common.OutBox;
public class OutboxMessage
{
    public Guid Id { get; set; }
    public DateTime OccurredOn { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public StatusProcess StatusProcess{ get; set; }
    public DateTime? ProcessedDate { get; set; }
    public OutboxMessage(string type, string data)
    {
        Type = type;
        Data = data;
        OccurredOn = DateTime.Now;
        StatusProcess = StatusProcess.NonProcess;
    }

    public void ProcessMessage()
    {
        ProcessedDate = DateTime.Now;
        StatusProcess = StatusProcess.Processed;
    }
}

public enum StatusProcess
{
    Processed,
    NonProcess
}
