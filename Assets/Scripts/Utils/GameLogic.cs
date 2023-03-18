using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class GameLogic
    {
        /// <summary>
        /// All the combinations on our board thar produce a winning outcome
        /// </summary>
        private static readonly int[,] WinConditions = new int[8, 3]
        {
            {0, 1, 2},
            {3, 4, 5},
            {6, 7, 8},
            {0, 3, 6},
            {1, 4, 7},
            {2, 5, 8},
            {0, 4, 8},
            {2, 4, 6}
        };

        /// <summary>
        /// Checks the provided board for winning conditions or returns a 'tie'
        /// if all moves are exhausted
        /// </summary>
        public static string CheckState(string[] board, int depth = 0)
        {
            // Loop from all possible win conditions and verify if any apply
            for (var i = 0; i < WinConditions.GetLength(0); i++)
            {
                if (board[WinConditions[i, 0]] == board[WinConditions[i, 1]] && board[WinConditions[i, 1]] ==
                    board[WinConditions[i, 2]] && !string.IsNullOrEmpty(board[WinConditions[i, 0]]))
                {
                    return board[WinConditions[i, 0]];
                }
            }

            // Return a 'tie' if there aren't any available moves left
            return board.Count(string.IsNullOrEmpty) == 0 ? "tie" : null;
        }

        /// <summary>
        /// Calculate the next best possible move for the AI
        /// </summary>
        public static int BestMove(string[] board, int difficultyMode)
        {
            // Easy Difficulty
            if (difficultyMode == 0)
            {
                var rand = new Random();
                var availableSlots = new List<int>();
                for (var i = 0; i < board.Length; i++)
                {
                    if (!string.IsNullOrEmpty(board[i])) continue;
                    availableSlots.Add(i);
                }

                return availableSlots[rand.Next(0, availableSlots.Count)];
            }
            
            // Medium or Unbeatable Mode
            var best = -100;
            var move = -1;

            for (var i = 0; i < board.Length; i++)
            {
                if (!string.IsNullOrEmpty(board[i])) continue;
                board[i] = "O";
                var score = MiniMax(board, 0, false, difficultyMode);
                board[i] = "";
                if (score <= best) continue;
                best = score;
                move = i;
            }

            return move;
        }

        /// <summary>
        /// Use the MiniMax algorithm in order to assign scores to all possible outcomes for each available move 
        /// </summary>
        private static int MiniMax(string[] board, int depth, bool isMaximizing, int difficultyMode)
        {
            var result = CheckState(board, depth);
            if (!string.IsNullOrEmpty(result))
            {
                return result switch
                {
                    "X" => -10,
                    "O" => 10,
                    _ => 0
                };
            }

            // If easyMode is on we limit the depth of the AI tree
            if (difficultyMode == 1 && depth > 0)
            {
                return 0;
            }

            // If the move was made by the AI we want the maximum score
            if (isMaximizing)
            {
                var maxScore = -100;
                for (var i = 0; i < board.Length; i++)
                {
                    if (!string.IsNullOrEmpty(board[i])) continue;
                    board[i] = "O";
                    var score = MiniMax(board, depth + 1, false, difficultyMode) - (depth);
                    board[i] = "";
                    maxScore = Math.Max(score, maxScore);
                }

                return maxScore;
            }
            // Else, we want the minimum
            else
            {
                var minScore = 100;
                for (var i = 0; i < board.Length; i++)
                {
                    if (!string.IsNullOrEmpty(board[i])) continue;
                    board[i] = "X";
                    var score = MiniMax(board, depth + 1, true, difficultyMode) + (depth);
                    board[i] = "";
                    minScore = Math.Min(score, minScore);
                }

                return minScore;
            }
        }
    }
}