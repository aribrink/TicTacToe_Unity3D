using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiController : MonoBehaviour
    {
        #region Variables

        private GameManager _gameManager;

        [Title("References")] public Toggle settingsTab;
        public Toggle scoresTab;
        public Button startButton;
        public Button resetScoresButton;
        public Toggle vsCpuToggle;
        public Toggle cpuFirstToggle;
        public TMP_Dropdown handDropdown;
        public RectTransform uiRect;
        public TMP_Dropdown modeDropdown;
        public TextMeshProUGUI xScoreLabel;
        public TextMeshProUGUI oScoreLabel;

        #endregion

        #region Basic Methods

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            settingsTab.isOn = true;
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
            handDropdown.interactable = true;
            startButton.interactable = true;
            cpuFirstToggle.interactable = true;
            vsCpuToggle.interactable = true;
            modeDropdown.interactable = true;
            resetScoresButton.interactable = true;
        }

        private void DisableUI()
        {
            scoresTab.isOn = true;
            handDropdown.interactable = false;
            startButton.interactable = false;
            cpuFirstToggle.interactable = false;
            vsCpuToggle.interactable = false;
            modeDropdown.interactable = false;
            resetScoresButton.interactable = false;
        }

        #endregion

        #region UI Methods

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

        public void SetHand(int order)
        {
            uiRect.SetSiblingIndex(order);
        }

        public void SetScore(Vector2 scores)
        {
            if (int.Parse(xScoreLabel.text) != (int) scores.x)
            {
                xScoreLabel.text = $"{scores.x}";
                xScoreLabel.rectTransform.DOScale(Vector3.one, 0.3f).From(Vector3.zero).SetEase(Ease.OutBack);
            }

            if (int.Parse(oScoreLabel.text) != (int) scores.y)
            {
                oScoreLabel.text = $"{scores.y}";
                oScoreLabel.rectTransform.DOScale(Vector3.one, 0.3f).From(Vector3.zero).SetEase(Ease.OutBack);
            }
        }

        public void ResetScores()
        {
            _gameManager.ResetScores();
        }

        #endregion
    }
}