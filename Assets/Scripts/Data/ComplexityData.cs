using System;
using UnityEngine;

[Serializable]
public class ComplexityData
{
    [field:SerializeField] public ComplexityType Type { get; private set; }
    [field:SerializeField] public int MinStep { get; private set; }
    [field:SerializeField] public int MaxStep { get; private set; }
}