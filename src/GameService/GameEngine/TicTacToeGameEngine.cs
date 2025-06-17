using GameService.Models;

namespace GameService.GameEngine;

public class TicTacToeGameEngine
{
    public TicTacToeGame CreateNewGame()
    {
        return new TicTacToeGame();
    }

    public bool IsValidMove(TicTacToeGame game, int position)
    {
        return position >= 0 && position < 9 && game.Board[position] == '-';
    }

    public void MakeMove(TicTacToeGame game, int position)
    {
        if (!IsValidMove(game, position))
            throw new InvalidOperationException("Invalid move");

        game.Board[position] = game.CurrentPlayer;

        if (CheckWin(game.Board, game.CurrentPlayer))
        {
            game.IsCompleted = true;
            game.Winner = game.CurrentPlayer;
            game.State = game.CurrentPlayer == 'X' ? GameState.XWon : GameState.OWon;
        }
        else if (game.Board.All(c => c != '-'))
        {
            game.IsCompleted = true;
            game.Winner = null;
            game.State = GameState.Draw;
        }
        else
        {
            game.CurrentPlayer = game.CurrentPlayer == 'X' ? 'O' : 'X';
        }
    }

    private bool CheckWin(char[] board, char player)
    {
        int[,] wins = new int[,]
        {
            {0,1,2},{3,4,5},{6,7,8},
            {0,3,6},{1,4,7},{2,5,8},
            {0,4,8},{2,4,6}
        };

        for (int i = 0; i < wins.GetLength(0); i++)
        {
            if (board[wins[i, 0]] == player &&
                board[wins[i, 1]] == player &&
                board[wins[i, 2]] == player)
                return true;
        }
        return false;
    }
}