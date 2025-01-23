using System.Collections.Generic;
using System.Text;

public class LevelStateData
{
    public readonly int[][] InitialPosition;

    public readonly int DiskCount;

    public LevelStateData(int[][] initialPositions, int diskCount)
    {
        InitialPosition = initialPositions;

        DiskCount = diskCount;
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