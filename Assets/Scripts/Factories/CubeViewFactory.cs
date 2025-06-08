#nullable enable

using EnvLevel;
using MergeLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factories
{
    public class CubeViewFactory
    {
        private readonly MergeManager _mergeManager;
        private readonly CubeView _cubeViewPrefab;
        
        public CubeViewFactory(MergeManager mergeManager, CubeView cubeViewPrefab)
        {
            _mergeManager = mergeManager;
            _cubeViewPrefab = cubeViewPrefab;
        }
        
        public CubeView Create(Vector3 position, Quaternion rotation)
        {
            var cube = Object.Instantiate(_cubeViewPrefab, position, rotation);
            _mergeManager.AddCube(cube);

            return cube;
        }
    }
}