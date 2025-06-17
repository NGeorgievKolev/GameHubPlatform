using GameService.Services;
using StackExchange.Redis;
using System.Diagnostics;

namespace GameService.Tests
{
    public class RedisSessionStoreTests : IAsyncLifetime
    {
        private readonly RedisSessionStore _store;
        private readonly IConnectionMultiplexer _redis;

        public RedisSessionStoreTests()
        {
            _redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" },
                ConnectTimeout = 200,
                SyncTimeout = 200,
                AbortOnConnectFail = false
            });
            _store = new RedisSessionStore(_redis);
        }

        [Fact]
        public async Task SaveAndRetrieveGame_ShouldReturnSameGame()
        {
            // Arrange
            var game = new TicTacToeGame
            {
                SessionId = Guid.NewGuid().ToString(),
                CurrentPlayer = 'X'
            };

            // Act
            await _store.SaveSessionAsync(game);
            var retrieved = await _store.GetSessionAsync(game.SessionId);

            // Assert
            Assert.NotNull(retrieved);
            Assert.Equal(game.SessionId, retrieved!.SessionId);
            Assert.Equal(game.CurrentPlayer, retrieved.CurrentPlayer);
        }

        [Fact]
        public async Task DeleteSessionAsync_ShouldRemoveGame()
        {
            var game = new TicTacToeGame
            {
                SessionId = Guid.NewGuid().ToString(),
                CurrentPlayer = 'O'
            };

            await _store.SaveSessionAsync(game);
            await _store.DeleteSessionAsync(game.SessionId);

            var deleted = await _store.GetSessionAsync(game.SessionId);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task GetSessionAsync_WithInvalidId_ShouldReturnNull()
        {
            var result = await _store.GetSessionAsync("non-existent-session-id");
            Assert.Null(result);
        }

        [Fact]
        public async Task GetSessionAsync_WithInvalidId_ShouldReturnNull_Quickly()
        {
            var sw = Stopwatch.StartNew();
            var result = await _store.GetSessionAsync("non-existent-id");
            sw.Stop();

            Assert.Null(result);
            Assert.True(sw.ElapsedMilliseconds < 200, $"Took too long: {sw.ElapsedMilliseconds}ms");
        }

        public Task InitializeAsync() => Task.CompletedTask;
        public Task DisposeAsync()
        {
            _redis.Dispose();
            return Task.CompletedTask;
        }
    }
}
