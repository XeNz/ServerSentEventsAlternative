using System.Runtime.CompilerServices;
using ServerSentEventsAlternative.Channels;
using ServerSentEventsAlternative.Models;

namespace ServerSentEventsAlternative.Services;

public sealed class StockFeedService(StockChannel stockChannel)
{
    public async IAsyncEnumerable<Stock> ListenToStockFilteredAsync(string code, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var item in stockChannel.Stocks.Reader.ReadAllAsync(cancellationToken))
        {
            if (string.Equals(code, item.Code, StringComparison.OrdinalIgnoreCase))
            {
                yield return item;
            }
        }
    }
}