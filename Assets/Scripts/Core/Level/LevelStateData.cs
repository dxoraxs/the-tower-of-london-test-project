using System.Collections.Generic;

public class LevelStateData
{
    public readonly Stack<int>[] InitialPosition = { new(), new(), new() };

    public readonly int MaxCountStep;
    public readonly int FinalColumnIndex;

    public LevelStateData(int[][] initialPositions, int maxCountStep, int finalColumnIndex)
    {
        for (var columnIndex = 0;
             columnIndex < InitialPosition.Length && columnIndex < initialPositions.Length;
             columnIndex++)
        {
            for (var verticalIndex = 0; verticalIndex < initialPositions[columnIndex].Length; verticalIndex++)
            {
                InitialPosition[columnIndex].Push(initialPositions[columnIndex][verticalIndex]);
            }
        }

        MaxCountStep = maxCountStep;
        FinalColumnIndex = finalColumnIndex;
    }
}