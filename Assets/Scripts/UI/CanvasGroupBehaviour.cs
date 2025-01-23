using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public void SetActive(bool value)
    {
        _canvasGroup.alpha = value ? 1 : 0;
        _canvasGroup.interactable = value;
        _canvasGroup.blocksRaycasts = value;
    }

    private void OnValidate()
    {
        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();
    }
}