using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private ColumnController _columnController;
    [SerializeField] private DiskController _diskController;
    [SerializeField] private LevelSettings _levelSettings;
    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private EndScreen _endScreen;
    private readonly List<Column> _columns = new();
    private LevelStateData _levelStateData;
    private readonly ReactiveProperty<int> _stepCounter = new();

    private void SetRandomDisks()
    {
        var levelGenerator = new LevelGenerator(_levelSettings, 3);
        _levelStateData = levelGenerator.GetNewLevelStateData();
        _stepCounter.Value = _levelStateData.StepCount;
        Debug.Log(_levelStateData);

        _diskController.DestroyDisks();
        _diskController.InitializeDisks(_levelStateData.DiskCount);

        var diskPosition = new Dictionary<int, Vector3>();

        _columns.Clear();
        for (var x = 0; x < _levelStateData.InitialPosition.Length; x++)
        {
            var column = new Column();
            _columns.Add(column);
            for (var y = 0; y < _levelStateData.InitialPosition[x].Length; y++)
            {
                var diskIndex = _levelStateData.InitialPosition[x][y];
                if (diskIndex < 0) continue;

                var horizontalPosition = _columnController.GetHorizontalPositionByColumnIndex(x);
                var verticalPosition = _columnController.GetVerticalPositionByDiskIndex(y);

                var position = new Vector3(horizontalPosition, verticalPosition, 0);
                diskPosition.Add(diskIndex, position);

                column.Disks.Push(diskIndex);
            }
        }

        _diskController.FastSetDisksToColumn(diskPosition);
    }

    private void Start()
    {
        _stepCounter.Subscribe(_gameScreen.UpdateCounter);
        SetRandomDisks();
        WaitingColumnClick();
    }

    private async void WaitingColumnClick()
    {
        _gameScreen.ShowScreen();
        bool resultLevel;
        while (true)
        {
            var firstColumnIndex = await _columnController.OnColumnClick.First().ToUniTask();

            var resultFirstClick = await OnFirstClickColumn(firstColumnIndex);
            
            if (!resultFirstClick) continue;

            var secondColumnIndex = await _columnController.OnColumnClick.First().ToUniTask();

            await OnSecondClickColumn(firstColumnIndex, secondColumnIndex);
            
            if (CheckToEndLevel())
            {
                resultLevel = true;
                break;
            }
            
            _stepCounter.Value--;
            if (_stepCounter.Value == 0)
            {
                resultLevel = false;
                break;
            }
        }

        await UniTask.Delay(1000);
        _gameScreen.HideScreen();
        _endScreen.ShowScreen();
        _endScreen.SetResult(resultLevel);

        await _endScreen.OnClickRestart.First().ToUniTask();
        
        _endScreen.HideScreen();
        SetRandomDisks();
        WaitingColumnClick();
    }
    
    private bool CheckToEndLevel()
    {
        var countColumnsWithDisks = _columns.Where(x => x.Disks.Any());
        if (countColumnsWithDisks.Count() > 1) return false;

        var ints = countColumnsWithDisks.ElementAt(0).Disks.ToArray();
        for (var index = 1; index < ints.Length; index++)
        {
            if (ints[index - 1] - ints[index] > 1)
                return false;
        }

        return true;
    }

    private async UniTask<bool> OnFirstClickColumn(int columnIndex)
    {
        if (!_columns[columnIndex].Disks.Any())
            return false;
        var diskIndex = _columns[columnIndex].Disks.Peek();
        var higherPosition = GetHigherPosition(columnIndex);
        await _diskController.StartMoveToPoint(diskIndex, higherPosition);

        return true;
    }

    private async UniTask OnSecondClickColumn(int firstColumnIndex, int secondColumnIndex)
    {
        var disks = _columns[firstColumnIndex].Disks;
        var diskIndex = disks.Pop();

        if (firstColumnIndex == secondColumnIndex)
        {
            var higherPosition = _columnController.GetPositionByIndex(secondColumnIndex, disks.Count());
            await _diskController.StartMoveToPoint(diskIndex, higherPosition);
        }
        else
        {
            if (_columns[secondColumnIndex].Disks.Any() && _columns[secondColumnIndex].Disks.Peek() < diskIndex)
                secondColumnIndex = firstColumnIndex;
            
            var higherPosition = GetHigherPosition(secondColumnIndex);
            await _diskController.StartMoveToPoint(diskIndex, higherPosition);
            higherPosition = _columnController.GetPositionByIndex(secondColumnIndex, _columns[secondColumnIndex].Disks.Count());
            await _diskController.StartMoveToPoint(diskIndex, higherPosition);
        }
        
        _columns[secondColumnIndex].Disks.Push(diskIndex);
    }

    private Vector3 GetHigherPosition(int columnIndex)
    {
        var horizontalPosition = _columnController.GetHorizontalPositionByColumnIndex(columnIndex);
        var targetSelectPosition = new Vector3(horizontalPosition, 5, 0);
        return targetSelectPosition;
    }
}