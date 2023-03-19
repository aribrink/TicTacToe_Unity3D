using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class CellController : MonoBehaviour
    {
        [Title("References")] 
        public Button button;
        public Image buttonImage;
        public TextMeshProUGUI valueLabel;

        [Title("Settings")] 
        public Color xColor;
        public Color oColor;

        private GameManager _gameManager;
        private string _currentValue = "";
        private int _cellId;

        public void Init(in int id, GameManager manager)
        {
            _gameManager = manager;
            _cellId = id;
            
            name = $"{_cellId}";
        }
        
        private void Set(string value)
        {
            if (!string.IsNullOrEmpty(_currentValue)) return;
            _currentValue = value;
            button.interactable = false;
            buttonImage.color = _currentValue == "X" ? xColor : oColor;
            valueLabel.text = $"{_currentValue}";
            GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f).From(new Vector3(1.2f,1.2f,1)).SetEase(Ease.OutBack);
            valueLabel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.6f).From(Vector3.zero).SetEase(Ease.OutBack);
        }

        public void Move()
        {
            if (_gameManager.gameState != GameManager.GameState.Active) return;
            Set(_gameManager.GetCurrentPlayer());
            _gameManager.PlayerMove(_cellId);
        }
    }
}