using System.Collections.Generic;
using Tic_Tac_Toe.Data;

namespace Tic_Tac_Toe.Repository
{
    public static class GameLogic
    {
        public enum GameStatus
        {
            INCOMPLETE = -1,
            DRAW = 0,
            PLAYER1WON = 1,
            PLAYER2WON = 2
        }

        public static int[,] CreateBoard(IList<Move> movesList, Game game)
        {
            int[,] board = new int[3, 3];
            foreach (var move in movesList)
            {
                if (move.PlayerId == game.Player1Id)
                {
                    board[move.RowNumber, move.ColumnNumber] = 1;
                }
                else
                {
                    board[move.RowNumber, move.ColumnNumber] = 2;
                }
            }
            return board;
        }

        public static GameStatus CheckGameStatus(Move currentMove, Game game, int[,] board)
        {
            // Impossible to win or draw when total moves is less than 4
            if (game.NumberOfMoves <= 4)
            {
                return GameStatus.INCOMPLETE;
            }

            int playerCell = currentMove.PlayerId == game.Player1Id ? 1 : 2;
            GameStatus WinningPlayer = currentMove.PlayerId == game.Player1Id 
                                    ? GameStatus.PLAYER1WON 
                                    : GameStatus.PLAYER2WON;

            // Check Rows
            for (int i = 0; i < 3; i++)
            {
                if (board[currentMove.RowNumber, i] != playerCell) break;

                if (i == 2) return WinningPlayer;
            }

            // Check Columns
            for (int i = 0; i < 3; i++)
            {
                if (board[i, currentMove.ColumnNumber] != playerCell) break;

                if (i == 2) return WinningPlayer;
            }

            // Check Diaganol
            if (currentMove.RowNumber == currentMove.ColumnNumber)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (board[i, i] != playerCell) break;

                    if (i == 2) return WinningPlayer;
                }
            }

            // Check AntiDiaganol
            if (currentMove.RowNumber + currentMove.ColumnNumber == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (board[i, 2 - i] != playerCell) break;

                    if (i == 2) return WinningPlayer;
                }
            }

            // Check if current move is last move of the game
            if (game.NumberOfMoves == 9)
            {
                return GameStatus.DRAW;
            }

            return GameStatus.INCOMPLETE;
        }

        public static bool isPlayersTurn(Game game, Move currentMove)
        {
            var playerTurn = game.NumberOfMoves % 2 + 1;
            var requestedPlayer = game.Player1Id == currentMove.PlayerId ? 1 : 2;
            if (playerTurn == requestedPlayer)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public static bool isGameComplete(Game game)
        {
            return game.Status == (int) GameStatus.INCOMPLETE ? false : true;
        }

        public static List<string> CreateBoardRepresentation(int[,] boardMatrix)
        {
            return new List<string>
            {
                "[" + boardMatrix[0, 0] + ", " + boardMatrix[0, 1] + ", " + boardMatrix[0, 2] + "]",
                "[" + boardMatrix[1, 0] + ", " + boardMatrix[1, 1] + ", " + boardMatrix[1, 2] + "]",
                "[" + boardMatrix[2, 0] + ", " + boardMatrix[2, 1] + ", " + boardMatrix[2, 2] + "]"
            };     
        }
    }
}
