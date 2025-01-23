using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScreen : BaseScreen
{
    [SerializeField] private TMP_Text _stepCounter;

    public void UpdateCounter(int count)
    {
        _stepCounter.text = count.ToString();
    }
}
