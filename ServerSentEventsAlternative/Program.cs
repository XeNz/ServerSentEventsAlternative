using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ServerSentEventsAlternative.Channels;
using ServerSentEventsAlternative.Mapping;
using ServerSentEventsAlternative.Models;
using ServerSentEventsAlternative.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<StockChannel>();
builder.Services.AddHostedService<StockProducingService>();
builder.Services.AddScoped<StockFeedService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.MapPost("stocks/sse",
    static async (
        HttpContext ctx,
        [FromBody] StockFilter filter,
        StockFeedService stockFeedService,
        CancellationToken ct = default
    ) =>
    {
        ctx.Response.Headers.Add(HeaderNames.ContentType, "text/event-stream");
        ctx.Response.Headers.Add(HeaderNames.CacheControl, CacheControlHeaderValue.NoCacheString);
        ctx.Response.Headers.Add(HeaderNames.Connection, "keep-alive");

        await foreach (var stock in stockFeedService.ListenToStockFilteredAsync(filter.Code, ct))
        {
            await JsonSerializer.SerializeAsync(ctx.Response.Body, stock.MapToServerSentEvent());
            await ctx.Response.WriteAsync(string.Format("{0}{0}", Environment.NewLine));
            await ctx.Response.Body.FlushAsync();
        }

        return ValueTask.CompletedTask;
    });

app.UseSwagger();
app.UseSwaggerUI();

app.Run();