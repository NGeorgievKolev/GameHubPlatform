using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GameService.Services;

public class RabbitPublisher : IRabbitPublisher, IDisposable
{
    private readonly IConnection _connection;

    public RabbitPublisher(IConfiguration config)
    {
        var factory = new ConnectionFactory
        {
            HostName = config["RabbitMQ:Host"] ?? "localhost",
            Port = int.TryParse(config["RabbitMQ:Port"], out var port) ? port : 5672,
            UserName = config["RabbitMQ:Username"] ?? "guest",
            Password = config["RabbitMQ:Password"] ?? "guest"
        };

        _connection = factory.CreateConnection();
    }

    public Task PublishAsync<T>(string queue, T message)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(string.Empty, queue, null, body);
        return Task.CompletedTask;
    }

    public void Dispose() => _connection.Dispose();
}
