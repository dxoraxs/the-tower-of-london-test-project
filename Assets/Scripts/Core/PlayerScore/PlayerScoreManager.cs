using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class PlayerScoreManager
{
    private static readonly string filePath = Path.Combine(Application.persistentDataPath, "scores.json");

    public static void Save(string playerName, int score)
    {
        var playerScores = LoadAllScores();

        playerScores.Add(new PlayerScore(playerName, score));

        SaveScoresToFile(playerScores);
    }

    public static void Clear()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }


    public static PlayerScore[] Load(int countToLoad)
    {
        var playerScores = LoadAllScores();

        return playerScores.OrderByDescending(score => score.Score).Take(countToLoad).ToArray();
    }

    private static List<PlayerScore> LoadAllScores()
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);

            var loadedData = JsonUtility.FromJson<ScoreData>(json);
            return loadedData?.Scores ?? new List<PlayerScore>();
        }

        return new List<PlayerScore>();
    }

    private static void SaveScoresToFile(List<PlayerScore> playerScores)
    {
        var json = JsonUtility.ToJson(new ScoreData(playerScores), true);

        File.WriteAllText(filePath, json);
    }
}