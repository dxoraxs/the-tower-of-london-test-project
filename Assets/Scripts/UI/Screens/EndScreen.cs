using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : BaseScreen
{
    public readonly Subject<Unit> OnClickRestart = new();
    public event Action<string> OnClickSaveScore;
    [SerializeField] private CanvasGroupBehaviour _winView;
    [SerializeField] private CanvasGroupBehaviour _loseView;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _saveScoreButton;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private ScoreItemContainer[] _scoreItemContainers; 

    private void Start()
    {
        _restartButton.onClick.AddListener(OnClickRestartButton);
        _saveScoreButton.onClick.AddListener(OnClickSaveScoreButton);
    }

    public void SetScoreItem(PlayerScore[] players)
    {
        for (var i = 0; i < players.Length && i < _scoreItemContainers.Length; i++)
        {
            _scoreItemContainers[i].gameObject.SetActive(true);
            _scoreItemContainers[i].SetScore(players[i].PlayerName, players[i].Score);
        }

        for (var i = players.Length; i < _scoreItemContainers.Length; i++)
        {
            _scoreItemContainers[i].gameObject.SetActive(false);
        }
    }

    public override void HideScreen()
    {
        base.HideScreen();
        _inputField.text = String.Empty;
    }

    public void SetResult(bool value)
    {
        _winView.SetActive(value);
        _loseView.SetActive(!value);
    }

    private void OnClickSaveScoreButton()
    {
        OnClickSaveScore?.Invoke(_inputField.text);
    }

    private void OnClickRestartButton()
    {
        OnClickRestart.OnNext(Unit.Default);
    }
}