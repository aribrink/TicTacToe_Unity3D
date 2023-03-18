using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class BoardLinesController : MonoBehaviour
{
    [Title("References")] 
    public RectTransform lineTransform;
    
    private void OnEnable()
    {
        GameManager.OnGameStart += ShowLines;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= ShowLines;
    }

    private void ShowLines()
    {
        lineTransform.DOScale(Vector3.one, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack);
    }
}
