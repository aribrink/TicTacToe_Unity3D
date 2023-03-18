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
        private Vector2 cellPosition;

        public void Init(in int x, in int y, GameManager manager)
        {
            _gameManager = manager;
            cellPosition.x = x;
            cellPosition.y = y;
            name = $"{x}_{y}";
        }
        
        private void Set(string value)
        {
            if (!string.IsNullOrEmpty(_currentValue)) return;
            button.interactable = false;
            _currentValue = value;
            buttonImage.color = _currentValue == "X" ? xColor : oColor;
            valueLabel.text = $"{_currentValue}";
        }

        public void Move()
        {
            Set(_gameManager.GetCurrentPlayer());
            _gameManager.PlayerMove((int) cellPosition.x, (int) cellPosition.y);
        }
    }
}