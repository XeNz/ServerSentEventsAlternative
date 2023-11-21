using System.Threading.Channels;
using ServerSentEventsAlternative.Models;

namespace ServerSentEventsAlternative.Channels;

public class StockChannel
{
    public Channel<Stock> Stocks { get; }

    public StockChannel()
    {
        var channelOptions = new BoundedChannelOptions(5)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        Stocks = Channel.CreateBounded<Stock>(channelOptions);
    }
}