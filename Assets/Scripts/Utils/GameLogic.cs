using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = System.Random;


namespace Utils
{
    public class GameLogic
    {
        /// <summary>
        /// Checks the provided board for winning conditions ore returns 'tie'
        /// if all moves are exhausted
        /// </summary>
        public static string CheckWin(string[][] board)
        {
            // Debug.Log($"{board[2][0]}");
            var winningPlayer = "";
            // Test rows
            if (board[0][0] == board[0][1] && board[0][0] == board[0][2] && !string.IsNullOrEmpty(board[0][0]))
                winningPlayer = board[0][0];
            if (board[1][0] == board[1][1] && board[1][0] == board[1][2] && !string.IsNullOrEmpty(board[1][0]))
                winningPlayer = board[1][0];
            if (board[2][0] == board[2][1] && board[2][0] == board[2][2] && !string.IsNullOrEmpty(board[2][0]))
                winningPlayer = board[1][0];

            // Test Columns
            if (board[0][0] == board[1][0] && board[0][0] == board[2][0] && !string.IsNullOrEmpty(board[0][0]))
                winningPlayer = board[0][0];
            if (board[0][1] == board[1][1] && board[0][1] == board[2][1] && !string.IsNullOrEmpty(board[0][1]))
                winningPlayer = board[0][1];
            if (board[0][2] == board[1][2] && board[0][2] == board[2][2] && !string.IsNullOrEmpty(board[0][2]))
                winningPlayer = board[0][1];

            // Test Diagonals
            if (board[0][0] == board[1][1] && board[0][0] == board[2][2] && !string.IsNullOrEmpty(board[0][0]))
                winningPlayer = board[0][0];

            // Debug.Log($"{board[2][ 0]} | {board[1][ 1]} | {board[0][2]}");
            if (board[2][0] == board[1][1] && board[2][0] == board[0][2] && !string.IsNullOrEmpty(board[2][0]))
            {
                winningPlayer = board[0][0];
                // Debug.Log("diagonal wins");
            }

            if (!string.IsNullOrEmpty(winningPlayer))
            {
                return winningPlayer;
            }

            var emptyCells = 0;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(board[i][j]))
                        emptyCells++;
                }
            }

            return emptyCells == 0 ? "tie" : null;
        }

        public static (int row, int col) BestMove(string[][] board)
        {
            var open = 0;
            var best = -50;
            var move = (0, 0);
            // Debug.Log("--- Score Move ---");
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Debug.Log($"{i}:{j} | {string.IsNullOrEmpty(board[i][j])}");
                    if (string.IsNullOrEmpty(board[i][j]))
                    {
                        open++;
                        board[i][j] = "O";
                        var score = MiniMax(board, 0, false);
                        board[i][j] = "";
                        // Debug.Log($"{i}:{j} | {score}");
                        if (score > best)
                        {
                            best = score;
                            move = (i, j);
                        }
                    }
                }
            }

            Debug.Log(move);
            return move;
        }

        static int tests = 0;

        private static int MiniMax(string[][] board, int depth, bool isMaximizing)
        {
            tests++;
            var result = CheckWin(board);
            if (!string.IsNullOrEmpty(result))
            {
                // Debug.Log($"{tests}_  {result}");
                return result switch
                {
                    "X" => -10,
                    "O" => 10,
                    _ => 0
                };
            }

            // StringBuilder sb = new StringBuilder();
            // for(int i=0; i< board .GetLength(1); i++)
            // {
            //     for(int j=0; j<board .GetLength(0); j++)
            //     {
            //         sb.Append(board [i,j]);
            //         sb.Append(' ');				   
            //     }
            //     sb.AppendLine();
            // }
            // Debug.Log(sb.ToString());

            var rand = new Random();

            if (isMaximizing)
            {
                var bestScore = -50;
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (string.IsNullOrEmpty(board[i][j]))
                        {
                            board[i][j] = "O";
                            var score = MiniMax(board, depth + 1, false);
                            board[i][j] = "";
                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }

                return bestScore;
            }
            else
            {
                var bestScore = 50;
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (string.IsNullOrEmpty(board[i][j]))
                        {
                            // Debug.Log($"{i}_{j}");
                            board[i][j] = "X";
                            var score = MiniMax(board, depth + 1, true);

                            board[i][j] = "";
                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }

                return bestScore;
            }
        }
    }
}