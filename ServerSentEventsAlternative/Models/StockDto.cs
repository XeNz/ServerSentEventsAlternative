namespace ServerSentEventsAlternative.Models;

public record StockDto(string Code, decimal Value, DateTimeOffset Date) : IServerSentEventData
{
    public string EventName { get; } = "Stock";
}