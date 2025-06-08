#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class CubeColorsData
    {
        private readonly Dictionary<CubeLevel, Color> _colors;

        public CubeColorsData(Dictionary<CubeLevel, Color> colors)
        {
            _colors = colors;
        }

        public Color GetColor(CubeLevel cubeLevel)
        {
            if (_colors.TryGetValue(cubeLevel, out var color))
            {
                return color;
            }
            
            Debug.LogError($"Color for level {cubeLevel} not found.");
            
            return Color.white;
        }
    }
}