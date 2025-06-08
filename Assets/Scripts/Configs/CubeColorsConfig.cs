#nullable enable

using System;
using System.Linq;
using Data;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CubeColorsConfig", menuName = "Configs/CubeColorsConfig", order = 0)]
    public class CubeColorsConfig : ScriptableObject
    {
        [Serializable]
        private struct CubeColor
        {
            public CubeLevel Level;
            
            public Color Color;
        }
        
        [SerializeField]
        private CubeColor[] _cubeColors = null!;

        private CubeColorsData? _cache; 
        
        public CubeColorsData GetData()
        {
            if (_cache != null)
            {
                return _cache;
            }
            
            var colors = _cubeColors.ToDictionary(item => item.Level, item => item.Color);

            _cache = new CubeColorsData(colors);

            return _cache;
        }
    }
}