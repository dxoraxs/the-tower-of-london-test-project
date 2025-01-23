using System;

[Serializable]
public class PlayerScore
{
    public string PlayerName;
    public int Score;

    public PlayerScore(string playerName, int score)
    {
        PlayerName = playerName;
        Score = score;
    }
}