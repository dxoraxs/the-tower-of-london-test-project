using System;
using System.Collections.Generic;

[Serializable]
public class ScoreData
{
    public List<PlayerScore> Scores;

    public ScoreData(List<PlayerScore> scores)
    {
        this.Scores = scores;
    }
}