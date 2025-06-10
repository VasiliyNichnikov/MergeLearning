using System;
using System.Collections.Generic;
using Data;
using EnvLevel;
using UnityEngine;

namespace MergeLogic
{
    public class MergeManager
    {
        private const float MinMergingDistance = 0.5f;
        
        private readonly Dictionary<CubeLevel, CollisionGroup> _groups = new();
        
        private readonly CubeLevelsStorage _storage;
        private readonly LevelGeneration _levelGeneration;
        
        public MergeManager(LevelGeneration levelGeneration, CubeLevelsStorage storage)
        {
            _levelGeneration = levelGeneration;
            _storage = storage;
        }

        public void AddCube(ICubeController cube)
        {
            var initLevel = _levelGeneration.Generate();
            
            var changerColor = cube.ChangerColor;
            changerColor.ApplyColor(initLevel);
            
            _storage.Add(cube, initLevel);

            var group = GetOrCreateGroup(initLevel);
            cube.AddToGroup(group);
            
            cube.OnTryMerge += OnTryMerge;
        }

        private CollisionGroup GetOrCreateGroup(CubeLevel level)
        {
            if (_groups.TryGetValue(level, out var group))
            {
                return group;
            }
            
            _groups.Add(level, group = new CollisionGroup());

            return group;
        }

        private void OnTryMerge(ICubeController a, ICubeController b)
        {
            TryMergeInternal(a, b);
        }

        private void TryMergeInternal(ICubeController a, ICubeController b)
        {
            if (!_storage.CanMergeByLevel(a, b))
            {
                return;
            }
            
            var distance = Vector3.Distance(a.Position, b.Position);
            
            if (distance > MinMergingDistance)
            {
                return;
            }
            
            var (main, sucker) = GetMainAndSuckerBalls(a, b);

            UpdateLevelForCube(main);
            
            var suckerGroup = GetOrCreateGroup(_storage[sucker]);
            sucker.RemoveFromGroup(suckerGroup);
            sucker.Destroy();
        }

        private void UpdateLevelForCube(ICubeController cube)
        {
            var level = _storage[cube];
            var oldGroup = GetOrCreateGroup(level);
            cube.RemoveFromGroup(oldGroup);
            
            var nextLevel = _storage.GetNextLevel(level);
            _storage[cube] = nextLevel;
            cube.ChangerColor.ApplyColor(nextLevel);
            var newGroup = GetOrCreateGroup(nextLevel);
            cube.AddToGroup(newGroup);
        }

        private static (ICubeController Main, ICubeController Sucker) GetMainAndSuckerBalls(ICubeController first, ICubeController second)
        {
            var firstSpeedOfMovement = first.SpeedOfMovement;
            var secondSpeedOfMovement = second.SpeedOfMovement;

            return firstSpeedOfMovement > secondSpeedOfMovement 
                ? (first,second) 
                : (second, first);
        }
    }
}