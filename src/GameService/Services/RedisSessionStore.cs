using StackExchange.Redis;
using System.Text.Json;

namespace GameService.Services
{
    public class RedisSessionStore : ISessionStore
    {
        private readonly IDatabase _db;
        private const string SessionKeyPrefix = "game-session:";

        public RedisSessionStore(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<TicTacToeGame?> GetSessionAsync(string sessionId)
        {
            var value = await _db.StringGetAsync(SessionKeyPrefix + sessionId);
            if (!value.HasValue)
                return null;

            return JsonSerializer.Deserialize<TicTacToeGame>(value.ToString());

        }

        public async Task DeleteSessionAsync(string sessionId)
        {
            await _db.KeyDeleteAsync(SessionKeyPrefix + sessionId);
        }

        public async Task SaveSessionAsync(TicTacToeGame game)
        {
            if (string.IsNullOrEmpty(game.SessionId))
                throw new ArgumentException("SessionId is required", nameof(game.SessionId));

            var json = JsonSerializer.Serialize(game);
            await _db.StringSetAsync(SessionKeyPrefix + game.SessionId, json, TimeSpan.FromMinutes(15));
        }

    }
}
