using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiController : MonoBehaviour
    {
        private GameManager _gameManager;

        [Title("References")] 
        public ToggleGroup tabs;
        public Toggle settingsTab;
        public Toggle scoresTab;
        public Button startButton;
        public Button resetScoresButton;
        public Toggle vsCpuToggle;
        public Toggle cpuFirstToggle;
        public TMP_Dropdown modeDropdown;
        public TextMeshProUGUI xScoreLabel;
        public TextMeshProUGUI oScoreLabel;
        
        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            tabs.NotifyToggleOn(settingsTab);
        }

        private void OnEnable()
        {
            GameManager.OnGameStart += DisableUI;
            GameManager.OnGameEnd += EnableUI;
        }

        private void OnDisable()
        {
            GameManager.OnGameStart += DisableUI;
            GameManager.OnGameEnd -= EnableUI;
        }

        private void EnableUI()
        {
            startButton.interactable = true;
            cpuFirstToggle.interactable = true;
            vsCpuToggle.interactable = true;
            modeDropdown.interactable = true;
            resetScoresButton.interactable = true;
        }
        
        private void DisableUI()
        {
            scoresTab.isOn = true;
            startButton.interactable = false;
            cpuFirstToggle.interactable = false;
            vsCpuToggle.interactable = false;
            modeDropdown.interactable = false;
            resetScoresButton.interactable = false;

        }

        public void StartGame()
        {
            _gameManager.Restart();
        }

        public void SetDifficulty(int state)
        {
            _gameManager.difficultyMode = state;
        }

        public void SetVersusCpuMode(bool state)
        {
            _gameManager.vsCpu = state;
            cpuFirstToggle.interactable = state;
        }

        public void SetFirstPlayerCpu(bool state)
        {
            _gameManager.cpuPlaysFirst = state;
        }

        public void SetScore(Vector2 scores)
        {
            xScoreLabel.text = $"{scores.x}";
            oScoreLabel.text = $"{scores.y}";
        }

        public void ResetScores()
        {
            _gameManager.ResetScores();
        }
    }
}