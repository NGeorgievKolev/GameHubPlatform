using GameService.Models;

public class TicTacToeGame
{
    public string SessionId { get; set; } = Guid.NewGuid().ToString();
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public char[] Board { get; set; } = Enumerable.Repeat('-', 9).ToArray();
    public char CurrentPlayer { get; set; } = 'X';
    public bool IsCompleted { get; set; }
    public char? Winner { get; set; }
    public GameState State { get; set; } = GameState.InProgress;
}
