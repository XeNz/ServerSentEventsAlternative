using System.Text.Json.Serialization;

namespace ServerSentEventsAlternative.Models;

public record StockDto(string Code, decimal Value, DateTimeOffset Date) : IServerSentEventData
{
    [JsonIgnore]
    public string EventName { get; } = "Stock";
}