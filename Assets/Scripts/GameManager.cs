using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using Utils;

public class GameManager : SerializedMonoBehaviour
{
    public enum GameState
    {
        Locked,
        Active
    }
    
    // Game state of the game. "Locked state" disables User Input
    public GameState gameState = GameState.Locked;
    
    [Title("Settings")] 
    public bool vsCpu = true;
    public bool cpuPlaysFirst = false;
    public int difficultyMode = 0;

    [Title("References")] 
    public Transform grid;
    public GameObject cellPrefab;

    // Public actions For components that might need them
    public static Action OnGameStart;
    public static Action OnGameEnd;

    private readonly string[] _cells = new string[9];
    private readonly CellController[] _cellControllers = new CellController[9];
    private const string Player1 = "X";
    private const string Player2 = "O";
    private string _currentPlayer;


    #region Basic Methods

    public string GetCurrentPlayer() => _currentPlayer;

    private void Start()
    {
        // Restart();
    }

    public void Restart()
    {
        // Fire the OnGameStart Event
        OnGameStart?.Invoke();

        // Reset game values
        gameState = GameState.Active;
        _currentPlayer = cpuPlaysFirst ? Player2 : Player1;
        Clear();
        CreateGrid();

        // Initiate the cpu move if it starts first
        if (_currentPlayer == Player2 && vsCpu)
            FindBestMove();
        else
            OutcomePopupController.Show("Begin");
    }

    private void CreateGrid()
    {
        // Instantiate all cells in the board
        for (var i = 0; i < _cells.Length; i++)
        {
            var cell = Instantiate(cellPrefab, grid);
            var cellController = cell.GetComponent<CellController>();
            cellController.Init(i, this);
            _cellControllers[i] = cellController;
        }
    }

    private void Clear()
    {
        // Clear the cell controller
        Array.Clear(_cellControllers, 0, _cellControllers.Length);

        // Clear the buttons
        foreach (Transform cell in grid)
        {
            Destroy(cell.gameObject);
        }

        // Clear the played values
        for (var i = 0; i < _cells.Length; i++)
        {
            _cells[i] = "";
        }
    }

    #endregion


    #region Game Methods

    public void PlayerMove(int cellId)
    {
        _cells[cellId] = _currentPlayer;
        EndTurn();
    }

    private void EndTurn()
    {
        var result = GameLogic.CheckState(_cells);

        if (!string.IsNullOrEmpty(result))
        {
            gameState = GameState.Locked;
            OutcomePopupController.OnComplete = () => OnGameEnd?.Invoke(); 
            OutcomePopupController.Show(result);
            return;
        }

        _currentPlayer = _currentPlayer == Player1 ? Player2 : Player1;
        if (_currentPlayer == Player2 && vsCpu)
            FindBestMove();
    }

    private void FindBestMove()
    {
        gameState = GameState.Locked;
        var bestMove = GameLogic.BestMove(_cells, difficultyMode);
        StartCoroutine(PlayCpuMove(bestMove));
    }

    // We use a coroutine to introduce a small delay to CPU movements
    private IEnumerator PlayCpuMove(int bestMoveId)
    {
        ThinkingController.Show();
        yield return new WaitForSeconds(1.5f);
        ThinkingController.Hide();
        yield return new WaitForSeconds(0.7f);
        gameState = GameState.Active;
        _cellControllers[bestMoveId].Move();
    }

    #endregion
}