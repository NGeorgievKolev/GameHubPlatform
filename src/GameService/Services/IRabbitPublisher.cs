namespace GameService.Services;

public interface IRabbitPublisher
{
    Task PublishAsync<T>(string queue, T message);
}