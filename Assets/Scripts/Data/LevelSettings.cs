using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "complexity_settings", menuName = "ComplexitySettings", order = 0)]
public class LevelSettings : ScriptableObject
{
    [field: SerializeField] public int ThresholdStepCount = 4;
    [SerializeField] private List<ComplexityData> _complexityData;

    public IReadOnlyList<ComplexityData> ComplexityData => _complexityData;
}