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
        public Button startButton;
        public Toggle vsCpuToggle;
        public Toggle cpuFirstToggle;
        public TMP_Dropdown modeDropdown;
        
        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
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
        }
        
        private void DisableUI()
        {
            startButton.interactable = false;
            cpuFirstToggle.interactable = false;
            vsCpuToggle.interactable = false;
            modeDropdown.interactable = false;
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
    }
}