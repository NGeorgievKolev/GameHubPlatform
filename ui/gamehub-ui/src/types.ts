export interface TicTacToeGame {
  id: string;
  sessionId: string;
  board: string[];
  currentPlayer: 'X' | 'O';
  isCompleted: boolean;
  winner: 'X' | 'O' | null;
  state: 'InProgress' | 'Completed';
}
