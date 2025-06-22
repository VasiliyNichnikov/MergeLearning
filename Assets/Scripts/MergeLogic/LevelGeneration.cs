#nullable enable

using System;
using Data;
using Random = UnityEngine.Random;

namespace MergeLogic
{
    public class LevelGeneration
    {
        public CubeLevel Generate()
        {
            var index = Random.Range(0, Enum.GetValues(typeof(CubeLevel)).Length - 1);
            return (CubeLevel)index;
        }
    }
}