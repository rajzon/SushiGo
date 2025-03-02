using System.Text.Json;
using System.Text.Json.Serialization;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using RabbitMQ.Client;
using Scalar.AspNetCore;

namespace SushiGo.Orders.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
        });
        builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
        builder.Services.AddOutputCache(options =>
        {
            options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(20)));
            options.AddPolicy("Expire5", builder => builder.Expire(TimeSpan.FromSeconds(50)).Tag("tag-id"));
        });

        var app = builder.Build();
        app.UseOutputCache();
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "/openapi/{documentName}.json";
        });
        app.MapScalarApiReference(options =>
        {
            options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });

        var sampleTodos = new Todo[]
        {
            new(1, "Walk the dog"),
            new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
            new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
            new(4, "Clean the bathroom"),
            new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
        };

        var todosApi = app.MapGroup("/todos");
        todosApi.MapGet("/",() => sampleTodos).CacheOutput();
        todosApi.MapGet("/{id}", [OutputCache(PolicyName = "Expire5")](int id) =>
            sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
                ? Results.Ok(todo)
                : Results.NotFound());
        
        todosApi.MapPost("/purge/{tag}", async (IOutputCacheStore cache, string tag) =>
        {
            //by using cache store we can evict outpu cache based on assiged to it tag
            await cache.EvictByTagAsync(tag, default);
        });
        
        // todosApi.MapGet("/etag", (context) =>
        // {
        //     var etag = $"\"{Guid.NewGuid():n}\"";
        //     context.Response.Headers.ETag = etag;
        //     var bytes = System.Text.Encoding.UTF8.GetBytes(etag);
        //     return
        // }).CacheOutput();

        todosApi.MapPost("/rabbit-direct", async (RabbitRequest request, [FromServices] IRabbitMqClient rabbitMqClient) =>
        {
            var channel = await rabbitMqClient.CreateChannelAsync();
            var exchangeName = "my_direct_exchange";
            var queueName = "my_direct_queue";
            var routingKey = "my_direct_routingkey";
            await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
            await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            await channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: routingKey, arguments: null);
            var evtWrapper = new CloudEvent
            {
                Type = "my_cloud_event",
                Source = new Uri("http://localhost:5252"),
                Time = DateTimeOffset.Now,
                DataContentType = "application/json",
                Id = Guid.NewGuid().ToString(),
                Data = request
            };
            var evtFormatter = new JsonEventFormatter();
            var json = evtFormatter.ConvertToJsonElement(evtWrapper).ToString();
            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(json);

            var props = new BasicProperties();
            props.ContentType = "application/json";
            props.DeliveryMode = DeliveryModes.Persistent;
            await channel.BasicPublishAsync(
                exchange: exchangeName,
                routingKey: routingKey,
                mandatory: false,
                basicProperties: props,
                body: messageBodyBytes);
            return Results.Accepted();
        });

        app.Run();
    }
}

public record RabbitRequest(string Name, string Lastname);

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
[JsonSerializable(typeof(RabbitRequest))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}

public interface IRabbitMqClient
{
    Task<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default);
}

public class RabbitMqClient : IRabbitMqClient, IAsyncDisposable
{
    private static IConnection? _connection;

    public RabbitMqClient()
    {
        ConnectionFactory factory = new ConnectionFactory();
        factory.UserName = "guest";
        factory.Password = "guest";
        factory.VirtualHost = "/";
        factory.HostName = "localhost";

        _connection = factory.CreateConnectionAsync().Result;
    }

    public Task<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default) =>
        _connection!.CreateChannelAsync(cancellationToken: cancellationToken);

    public async ValueTask DisposeAsync()
    {
        if (_connection is null)
        {
            return;
        }
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
    }
}