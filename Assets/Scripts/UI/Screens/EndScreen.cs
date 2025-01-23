using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : BaseScreen
{
    public readonly Subject<Unit> OnClickRestart = new();
    [SerializeField] private CanvasGroupBehaviour _winView;
    [SerializeField] private CanvasGroupBehaviour _loseView;
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        _restartButton.onClick.AddListener(OnClickRestartButton);
    }

    private void OnClickRestartButton()
    {
        OnClickRestart.OnNext(Unit.Default);
    }

    public void SetResult(bool value)
    {
        _winView.SetActive(value);
        _loseView.SetActive(!value);
    }
}
