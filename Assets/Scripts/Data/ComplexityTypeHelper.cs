using System;
using Random = UnityEngine.Random;

namespace Data
{
    public static class ComplexityTypeHelper
    {
        public static ComplexityType GetRandomComplexityType()
        {
            var values = Enum.GetValues(typeof(ComplexityType));
            return (ComplexityType)values.GetValue(Random.Range(0, values.Length));
        }
    }
}