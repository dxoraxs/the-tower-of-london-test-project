using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreItemContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;

    public void SetScore(string name, int score)
    {
        _name.text = name;
        _score.text = score.ToString();
    }
}