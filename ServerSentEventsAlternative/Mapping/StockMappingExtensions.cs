using ServerSentEventsAlternative.Models;

namespace ServerSentEventsAlternative.Mapping;

public static class StockMappingExtensions
{
    public static ServerSentEvent<StockDto> MapToServerSentEvent(this Stock stock)
    {
        var dto = new StockDto(stock.Code, stock.Value, stock.Date);
        var sse = new ServerSentEvent<StockDto>(Guid.NewGuid().ToString(), dto);
        return sse;
    }
}