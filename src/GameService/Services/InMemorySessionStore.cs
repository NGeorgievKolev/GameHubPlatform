using System.Collections.Concurrent;

namespace GameService.Services;

public class InMemorySessionStore : ISessionStore
{
    private readonly ConcurrentDictionary<string, TicTacToeGame> _sessions = new();

    public Task<TicTacToeGame?> GetSessionAsync(string sessionId)
    {
        _sessions.TryGetValue(sessionId, out var game);
        return Task.FromResult(game);
    }

    public Task SaveSessionAsync(TicTacToeGame game)
    {
        _sessions[game.Id] = game;
        return Task.CompletedTask;
    }

    public Task DeleteSessionAsync(string sessionId)
    {
        _sessions.TryRemove(sessionId, out _);
        return Task.CompletedTask;
    }
}