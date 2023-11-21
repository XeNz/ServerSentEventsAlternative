namespace ServerSentEventsAlternative.Models;

/// <summary>
/// Fictive stock value at a certain point in time
/// </summary>
/// <param name="Code">Stock NASDAQ Code</param>
/// <param name="Value">Stock price</param>
/// <param name="Date">Moment in time</param>
public sealed record Stock(string Code, decimal Value, DateTimeOffset Date);