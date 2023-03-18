using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ThinkingController : MonoBehaviour
{
    public static ThinkingController instance;
    
    [Title("References")] 
    public RectTransform eyeImage;

    
    private void Awake()
    {
        instance = this;
        eyeImage.localScale = Vector3.zero;
    }

    public static void Show()
    {
        instance.eyeImage.DOScale(Vector3.one, 0.3f).From(Vector3.zero).SetDelay(0.5f).SetEase(Ease.OutBack);
    }
    
    public static void Hide()
    {
        instance.eyeImage.DOScale(Vector3.zero, 0.3f).From(Vector3.one).SetEase(Ease.InBack);
    }
}
