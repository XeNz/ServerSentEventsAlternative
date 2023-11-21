namespace ServerSentEventsAlternative.Models;

public record ServerSentEvent<T>(string Id, T Data) where T : IServerSentEventData
{
    public string Event { get; init; } = Data.EventName;
}