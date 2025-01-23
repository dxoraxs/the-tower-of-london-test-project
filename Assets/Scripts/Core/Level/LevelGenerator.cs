using System.Collections.Generic;
using System.Linq;
using Data;
using Random = UnityEngine.Random;

public class LevelGenerator : ILevelGenerator
{
    private readonly List<int> _disks = new();
    private readonly List<int> _columns = new();
    private readonly Dictionary<ComplexityType, ComplexityData> _complexityData = new ();

    public LevelGenerator(LevelSettings levelSettings, int columnCount)
    {
        for (var i = 0; i < columnCount; i++)
        {
            _columns.Add(i);
        }
        
        foreach (var data in levelSettings.ComplexityData)
        {
            _complexityData.Add(data.Type, data);
        }
    }

    public LevelStateData GetNewLevelStateData()
    {
        var finalColumnIndex = _columns[Random.Range(0, _columns.Count)];
        var randomComplexity = RandomComplexity();
        var diskCount = randomComplexity.CountDisks;

        _disks.Clear();
        for (var i = 0; i < diskCount; i++)
        {
            _disks.Add(i);
        }
        
        var initialPosition = CalculateStartPosition(finalColumnIndex);
        
        var resultData = new LevelStateData(initialPosition, diskCount);

        return resultData;
    }

    private ComplexityData RandomComplexity()
    {
        var currentGameComplexityType = ComplexityTypeHelper.GetRandomComplexityType();
        return _complexityData[currentGameComplexityType];
    }

    private int[][] CalculateStartPosition(int finalColumnIndex)
    {
        var freeColumnIndex = new List<int>(_columns);
        var diskPositions = _columns.Select(i => new Stack<int>()).ToList();

        foreach (var diskType in _disks)
        {
            diskPositions[finalColumnIndex].Push(diskType);
        }

        var lastDiskType = int.MaxValue;

        for (var i = 0; i < _disks.Count; i++)
        {
            DoStep();
        }

        void DoStep()
        {
            var suitableColumns = diskPositions
                .Where(column => column.Any() && lastDiskType != column.Peek());
            if (!suitableColumns.Any())
            {
                return;
            }
            var randomPopColumn = suitableColumns.ElementAt(Random.Range(0, suitableColumns.Count()));
            var popColumnIndex = diskPositions.IndexOf(randomPopColumn); 
            lastDiskType = diskPositions[popColumnIndex].Pop();
            freeColumnIndex.Remove(popColumnIndex);

            var nextColumnIndex = freeColumnIndex[Random.Range(0, freeColumnIndex.Count)];

            diskPositions[nextColumnIndex].Push(lastDiskType);
            freeColumnIndex.Add(popColumnIndex);
        }

        return diskPositions.Select(x => x.ToArray()).ToArray();
    }
}