using System;
using System.Collections.Generic;
using Data;
using EnvLevel;

namespace MergeLogic
{
    public class MergeManager : IDisposable
    {
        private readonly Dictionary<CubeLevel, CubeGroupCollider> _groups = new();
        
        private readonly CubeSceneStorage _cubeStorage;
        private readonly CubeLevel _maxLevel;
        
        public MergeManager(CubeSceneStorage cubeStorage)
        {
            _cubeStorage = cubeStorage;

            _maxLevel = ((CubeLevel[])Enum.GetValues(typeof(CubeLevel)))[^1];
            _cubeStorage.OnCubeAdded += OnCubeAdded;
        }

        private void OnCubeAdded(ICubeController cube)
        {
            var cubeLevel = _cubeStorage[cube];

            if (cubeLevel == _maxLevel)
            {
                return;
            }
            
            var group = GetOrCreateGroup(cubeLevel);

            cube.AddToGroup(group);

            cube.OnTriggerWithOtherCube += otherBall =>
            {
                TryMerge(cube, otherBall);
            };
        }

        public void Dispose()
        {
            _cubeStorage.OnCubeAdded -= OnCubeAdded;
        }

        private CubeGroupCollider GetOrCreateGroup(CubeLevel level)
        {
            if (_groups.TryGetValue(level, out var group))
            {
                return group;
            }

            _groups[level] = new CubeGroupCollider();

            return _groups[level];
        }

        private void TryMerge(ICubeController cubeA, ICubeController cubeB)
        {
            if (!CanMerge(cubeA, cubeB))
            {
                return;
            }

            var oldCubeLevel = _cubeStorage[cubeA];
            var oldGroup = GetOrCreateGroup(oldCubeLevel);
            
            var nextLevel = GetNextLevel(oldCubeLevel);
            var nextGroup = GetOrCreateGroup(nextLevel);

            var (main, sucker) = GetMainAndSucker(cubeA, cubeB);
            
            main.ChangerColor.ApplyColor(nextLevel);
            main.RemoveFromGroup(oldGroup);
            _cubeStorage[main] = nextLevel;

            if (nextLevel != _maxLevel)
            {
                main.AddToGroup(nextGroup);
            }
            
            sucker.RemoveFromGroup(oldGroup);
            sucker.DestroySelf();
        }

        private bool CanMerge(ICubeController cubeA, ICubeController cubeB)
        {
            var cubeALevel = _cubeStorage[cubeA];
            var cubeBLevel = _cubeStorage[cubeB];

            return cubeALevel == cubeBLevel && _maxLevel != cubeALevel;
        }

        private static (ICubeController Main, ICubeController Sucker) GetMainAndSucker(
            ICubeController cubeA,
            ICubeController cubeB)
        {
            return cubeB.IsSelected ? (cubeB, cubeA) : (cubeA, cubeB);
        }

        private static CubeLevel GetNextLevel(CubeLevel currentLevel)
        {
            var currentLevelInt = (int)currentLevel;
            currentLevelInt++;

            return (CubeLevel)currentLevelInt;
        }
    }
}