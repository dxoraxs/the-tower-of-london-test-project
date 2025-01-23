using System.Collections.Generic;
using System.Text;

public class LevelStateData
{
    public readonly int[][] InitialPosition;

    public readonly int DiskCount;
    public readonly int StepCount;

    public LevelStateData(int[][] initialPositions, int diskCount, int stepCount)
    {
        InitialPosition = initialPositions;

        DiskCount = diskCount;
        StepCount = stepCount;
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();

        foreach (var column in InitialPosition)
        {
            foreach (var i in column)
            {
                strBuilder.Append($"[{i}]\t");
            }

            for (var i = column.Length; i < DiskCount; i++)
            {
                strBuilder.Append("[  ]\t");
            }

            strBuilder.AppendLine();
        }

        return strBuilder.ToString();
    }
}