using GameService.GameEngine;
using GameService.Services;
using StackExchange.Redis;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGameServiceCore(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<TicTacToeGameEngine>();
        services.AddSingleton<GameSessionService>();
        services.AddSingleton<IRabbitPublisher, RabbitPublisher>();

        if (!string.IsNullOrEmpty(config["Redis:Host"]))
        {
            var redisHost = config["Redis:Host"] ?? throw new ArgumentNullException("Redis:Host");
            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(redisHost));


            services.AddSingleton<ISessionStore, RedisSessionStore>();
        }
        else
        {
            services.AddSingleton<ISessionStore, InMemorySessionStore>();
        }

        services.AddControllers();
        return services;
    }
}
