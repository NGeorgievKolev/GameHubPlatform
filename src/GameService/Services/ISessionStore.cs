namespace GameService.Services;

public interface ISessionStore
{
    Task<TicTacToeGame?> GetSessionAsync(string sessionId);
    Task SaveSessionAsync(TicTacToeGame game);
    Task DeleteSessionAsync(string sessionId);
}