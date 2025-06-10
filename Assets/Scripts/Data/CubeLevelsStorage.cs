#nullable enable

using System;
using System.Collections.Generic;
using EnvLevel;
using UnityEngine;

namespace Data
{
    public class CubeLevelsStorage : ICubeLevelsReadOnly
    {
        private static readonly CubeLevel[] Levels = (CubeLevel[])Enum.GetValues(typeof(CubeLevel));
        
        private readonly Dictionary<ICubeController, CubeLevel> _cubeLevels = new();
        private readonly CubeLevel _maxLevel = Levels[^1];

        public CubeLevel this[ICubeController cube]
        {
            get
            {
                if (_cubeLevels.TryGetValue(cube, out var level))
                {
                    return level;
                }

                throw new Exception($"The cube with the level {level} was not found.");
            }

            set => _cubeLevels[cube] = value;
        }
        
        public void Add(ICubeController cube, CubeLevel level)
        {
            if (_cubeLevels.TryAdd(cube, level))
            {
                cube.OnBeforeDestroy += () => _cubeLevels.Remove(cube);
            }
            else
            {
                Debug.LogError("CubeLevelsStorage.Add: cube already contains in cubeLevels.");
            }
        }
        
        public CubeLevel GetNextLevel(CubeLevel a)
        {
            var levelIndex = (int)a;
            var nextLevel = Levels[Math.Min(Levels.Length - 1, levelIndex + 1)];
            
            return nextLevel;
        }
        
        public bool CanMergeByLevel(ICubeController a, ICubeController b)
        {
            if (!_cubeLevels.ContainsKey(a) || !_cubeLevels.ContainsKey(b))
            {
                return false;
            }
            
            var aLevel = _cubeLevels[a];
            var bLevel = _cubeLevels[b];

            if (aLevel == _maxLevel || bLevel == _maxLevel)
            {
                return false;
            }
            
            return aLevel == bLevel;
        }
    }
}