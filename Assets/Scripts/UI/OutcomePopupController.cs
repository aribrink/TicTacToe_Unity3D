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
        public static OutcomePopupController Instance;

        [Title("References")] public RectTransform root;
        public TextMeshProUGUI label;
        public Image backgroundImage;

        public Color xColor;
        public Color oColor;
        public Color tieColor;

        private void Awake()
        {
            Instance = this;
        }

        public static void Show(string text)
        {
            switch (text)
            {
                case "tie":
                    Instance.backgroundImage.color = Instance.tieColor;
                    text = "TIE";
                    break;
                case "X":
                    Instance.backgroundImage.color = Instance.xColor;
                    text = $"{text} Wins!";
                    break;
                case "O":
                    Instance.backgroundImage.color = Instance.oColor;
                    text = $"{text} Wins!";
                    break;
            }
            
            Instance.label.text = text;
            Instance.root.DOAnchorPosX(0, 0.5f).From(new Vector2(800, 0)).SetEase(Ease.OutBack).SetDelay(0.5f);
            Instance.root.DOAnchorPosX(-800, 0.5f).SetEase(Ease.InBack).SetDelay(3);
        }
    }
}