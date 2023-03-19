using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OutcomePopupController : MonoBehaviour
    {
        private static OutcomePopupController _instance;
        
        [Title("References")] public RectTransform root;
        public TextMeshProUGUI label;
        public Image backgroundImage;
        public Color xColor;
        public Color oColor;
        public Color tieColor;

        public static Action OnComplete; 
        
        private void Awake()
        {
            _instance = this;
        }

        public static void Show(string text)
        {
            switch (text)
            {
                case "Begin":
                    _instance.backgroundImage.color = _instance.tieColor;
                    text = "Begin!";
                    break;
                case "tie":
                    _instance.backgroundImage.color = _instance.tieColor;
                    text = "TIE";
                    break;
                case "X":
                    _instance.backgroundImage.color = _instance.xColor;
                    text = $"{text} Wins!";
                    break;
                case "O":
                    _instance.backgroundImage.color = _instance.oColor;
                    text = $"{text} Wins!";
                    break;
            }
            
            _instance.label.text = text;
            _instance.root.DOAnchorPosX(0, 0.5f).From(new Vector2(900, 0)).SetEase(Ease.OutBack).SetDelay(0.5f);
            _instance.root.DOAnchorPosX(-900, 0.5f).SetEase(Ease.InBack).SetDelay(3).OnComplete(()=> OnComplete?.Invoke());
        }
    }
}