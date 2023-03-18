using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

public class GameManager : SerializedMonoBehaviour
{
    
    [Title("Settings")] 
    public bool player1IsAi = false;
    public bool player2IsAi = true;

    [Title("References")] 
    public Transform grid;
    public GameObject cellPrefab;

    [Space(20)] [TableMatrix] 
    public string[][] cells = new string[3][];
    
    public string GetCurrentPlayer() => _currentPlayer;
    
    private Dictionary<string, CellController> _boardControllers = new Dictionary<string, CellController>();
    private const string Player1 = "X";
    private const string Player2 = "O";
    private string _currentPlayer;
    private int _currentMove = 0;
    

    #region Basic Methods

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        cells[0] = new string[3];
        cells[1] = new string[3];
        cells[2] = new string[3];
        _currentPlayer = Player1;
        _currentMove = 0;
        Clear();
        CreateGrid();
    }

    private void CreateGrid()
    {
        // Instantiate all cells in the board
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                var cell = Instantiate(cellPrefab, grid);
                var cellController = cell.GetComponent<CellController>();
                cellController.Init(i, j, this);
                _boardControllers[cellController.name] = cellController;
            }
        }
    }

    private void Clear()
    {
        // Clear the cell controller
        _boardControllers.Clear();

        // Clear the buttons
        foreach (Transform cell in grid)
        {
            Destroy(cell.gameObject);
        }

        // Clear the played values
        
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                cells[i][j] = "";
            }
        }
    }

    #endregion


    #region Game Logic

    public void PlayerMove(int row, int column)
    {
        _currentMove++;
        cells[row][column] = _currentPlayer;
        Debug.Log(cells[row][column]);
        EndTurn();
    }

    private void EndTurn()
    {
        var result = GameLogic.CheckWin(cells);

        if (!string.IsNullOrEmpty(result))
        {
            Debug.Log(result == "tie" ? "Tie" : $"{result} wins!");
            return;
        }
        
        _currentPlayer = _currentPlayer == Player1 ? Player2 : Player1;
        if (_currentPlayer == Player2 && player2IsAi)
            FindBestMove();
    }

    private void FindBestMove()
    {
        var bestMove = GameLogic.BestMove(cells);
        _boardControllers[$"{bestMove.col}_{bestMove.row}"].Move();
    }
    

    #endregion

    
}