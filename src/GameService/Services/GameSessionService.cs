using GameService.DTOs;
using GameService.GameEngine;
using GameService.Models;

namespace GameService.Services;

public class GameSessionService
{
    private readonly ISessionStore _store;
    private readonly TicTacToeGameEngine _engine;
    private readonly IRabbitPublisher _publisher;

    public GameSessionService(ISessionStore store, TicTacToeGameEngine engine, IRabbitPublisher publisher)
    {
        _store = store;
        _engine = engine;
        _publisher = publisher;
    }

    public async Task<string> CreateGameAsync()
    {
        var game = _engine.CreateNewGame();
        game.SessionId = game.Id;
        await _store.SaveSessionAsync(game);
        return game.Id;
    }


    public Task<TicTacToeGame?> GetGameAsync(string id) => _store.GetSessionAsync(id);

    public async Task<TicTacToeGame?> MakeMoveAsync(string id, int position)
    {
        var game = await _store.GetSessionAsync(id);
        if (game == null) return null;
        if (game.IsCompleted) return game;
        if (!_engine.IsValidMove(game, position)) return game;

        _engine.MakeMove(game, position);
        await _store.SaveSessionAsync(game);

        if (game.IsCompleted)
        {
            var result = game.State switch
            {
                GameState.XWon => "X",
                GameState.OWon => "O",
                _ => "Draw"
            };
            await _publisher.PublishAsync("GameEnded", new GameResultEvent { GameId = game.Id, Result = result });
        }

        return game;
    }
}