#nullable enable

using Data;
using EnvLevel;
using MergeLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factories
{
    public class CubeViewFactory
    {
        private readonly CubeSceneStorage _cubeStorage;
        private readonly CubeView _cubeViewPrefab;
        private readonly LevelGeneration _levelGeneration;
        
        public CubeViewFactory(CubeSceneStorage cubeStorage, LevelGeneration levelGeneration, CubeView cubeViewPrefab)
        {
            _cubeStorage = cubeStorage;
            _levelGeneration = levelGeneration;
            _cubeViewPrefab = cubeViewPrefab;
        }
        
        public CubeView Create(Vector3 position, Quaternion rotation)
        {
            var cube = Object.Instantiate(_cubeViewPrefab, position, rotation);
            var initLevel = _levelGeneration.Generate();
            
            var changerColor = cube.ChangerColor;
            changerColor.ApplyColor(initLevel);
            _cubeStorage.AddCube(cube, initLevel);

            return cube;
        }
    }
}