#nullable enable

using System;
using System.Collections.Generic;
using EnvLevel;
using UnityEngine;

namespace Data
{
    public class CubeSceneStorage
    {
        public event Action<ICubeController>? OnCubeAdded;

        public CubeLevel this[ICubeController cube]
        {
            get
            {
                if (_cubes.TryGetValue(cube, out var level))
                {
                    return level;
                }

                Debug.LogError($"CubeSceneStorage.GetCubeLevel: cube with level {level} not found.");

                return CubeLevel.Level_1;
            }
            set => _cubes[cube] = value;
        }
        
        private readonly Dictionary<ICubeController, CubeLevel> _cubes = new ();

        public void AddCube(ICubeController cube, CubeLevel level)
        {
            _cubes.Add(cube, level);

            OnCubeAdded?.Invoke(cube);
        }
    }
}