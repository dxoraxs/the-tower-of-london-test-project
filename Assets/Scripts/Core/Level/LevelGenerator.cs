using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Data;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class LevelGenerator : ILevelGenerator
{
    private readonly List<int> _disks = new();
    private readonly List<int> _columns = new();
    private readonly Dictionary<ComplexityType, ComplexityData> _complexityData = new ();
    private readonly int _thresholdStepCount;

    public LevelGenerator(LevelSettings levelSettings, int columnCount)
    {
        for (var i = 0; i < columnCount; i++)
        {
            _columns.Add(i);
        }

        _thresholdStepCount = levelSettings.ThresholdStepCount;
        foreach (var data in levelSettings.ComplexityData)
        {
            _complexityData.Add(data.Type, data);
        }
    }

    public LevelStateData GetNewLevelStateData()
    {
        var finalColumnIndex = _columns[Random.Range(0, _columns.Count)];
        var randomStepByComplexity = RandomStepByComplexity();
        var initialPosition = CalculateStartPosition(randomStepByComplexity, finalColumnIndex);
        
        var resultData = new LevelStateData(initialPosition, randomStepByComplexity, finalColumnIndex);

        return resultData;
    }

    private int RandomStepByComplexity()
    {
        var currentGameComplexityType = ComplexityTypeHelper.GetRandomComplexityType();
        var currentComplexityData = _complexityData[currentGameComplexityType];
        var randomStepByComplexity = Random.Range(currentComplexityData.MinStep, currentComplexityData.MaxStep + 1);
        return randomStepByComplexity;
    }

    private int[][] CalculateStartPosition(int stepCount, int finalColumnIndex)
    {
        var freeColumnIndex = new List<int>(_columns);
        var diskPositions = _columns.Select(i => new Stack<int>()).ToList();

        foreach (var diskType in _disks)
        {
            diskPositions[finalColumnIndex].Push(diskType);
        }

        var lastDiskType = int.MaxValue;

        for (; stepCount > 1; stepCount--)
        {
            DoStep();
        }

        void DoStep()
        {
            var popColumnIndex = diskPositions.FindIndex(column => column.Any() && lastDiskType != column.Peek()); 
            lastDiskType = diskPositions[popColumnIndex].Pop();
            freeColumnIndex.Remove(popColumnIndex);
            var nextColumnIndex = freeColumnIndex[Random.Range(0, freeColumnIndex.Count + 1)];
            diskPositions[nextColumnIndex].Push(lastDiskType);
            freeColumnIndex.Add(popColumnIndex);
        }

        return diskPositions.Select(x => x.ToArray()).ToArray();
    }
}