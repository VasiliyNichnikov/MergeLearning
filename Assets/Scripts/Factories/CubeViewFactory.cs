#nullable enable

using EnvLevel;
using Magnet;
using MergeLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factories
{
    public class CubeViewFactory
    {
        private readonly MergeManager _mergeManager;
        private readonly MagnetManager _magnetManager;
        private readonly CubeView _cubeViewPrefab;
        
        public CubeViewFactory(MergeManager mergeManager, MagnetManager magnetManager, CubeView cubeViewPrefab)
        {
            _mergeManager = mergeManager;
            _cubeViewPrefab = cubeViewPrefab;
            _magnetManager = magnetManager;
        }
        
        public CubeView Create(Vector3 position, Quaternion rotation)
        {
            var cube = Object.Instantiate(_cubeViewPrefab, position, rotation);
            _mergeManager.AddCube(cube);
            _magnetManager.AddCube(cube);

            return cube;
        }
    }
}