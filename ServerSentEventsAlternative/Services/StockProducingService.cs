using ServerSentEventsAlternative.Channels;
using ServerSentEventsAlternative.Models;

namespace ServerSentEventsAlternative.Services;

public sealed class StockProducingService(StockChannel stockChannel) : BackgroundService
{
    private readonly Random _rnd = new();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var tick = TimeSpan.FromSeconds(1);
        using PeriodicTimer timer = new(tick);

        while (!stoppingToken.IsCancellationRequested)
        {
            var stock = GenerateStock(_rnd);

            await stockChannel.Stocks.Writer.WriteAsync(stock, stoppingToken);
            await timer.WaitForNextTickAsync(stoppingToken);
        }

        stockChannel.Stocks.Writer.Complete();
    }

    private static Stock GenerateStock(Random rnd)
    {
        var dateTimeOffset = RandomDate(rnd);
        var stockCode = RandomStockCode(rnd);
        var price = RandomStockPrice(rnd);

        return new Stock(stockCode, price, dateTimeOffset);
    }

    private static decimal RandomStockPrice(Random rnd)
    {
        var pre = rnd.Next(0, 1000);
        var post = rnd.Next(1, 100);
        var stringified = $"{pre},{post}";
        return decimal.Parse(stringified);
    }

    private static string RandomStockCode(Random rnd) =>
        StockCodes.All[rnd.Next(0, StockCodes.All.Length)];

    private static DateTimeOffset RandomDate(Random rnd)
    {
        var now = DateTimeOffset.UtcNow;
        var yearAgo = DateTimeOffset.UtcNow.AddYears(-1);

        var timeSpan = now - yearAgo;
        var randomSpan = new TimeSpan(0, rnd.Next(0, (int)timeSpan.TotalMinutes), 0);

        return yearAgo.DateTime + randomSpan;
    }
}