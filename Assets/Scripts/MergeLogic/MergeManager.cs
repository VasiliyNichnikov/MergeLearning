using System;
using System.Collections.Generic;
using Data;
using EnvLevel;

namespace MergeLogic
{
    public class MergeManager
    {
        private readonly Dictionary<ICubeController, CubeLevel> _cubeLevels = new();
        private readonly Dictionary<CubeLevel, CollisionGroup> _groups = new();
        
        private readonly CubeLevel[] _levels = (CubeLevel[])Enum.GetValues(typeof(CubeLevel));
        
        private readonly LevelGeneration _levelGeneration;
        private readonly CubeLevel _maxLevel;
        
        public MergeManager(LevelGeneration levelGeneration)
        {
            _levelGeneration = levelGeneration;
            _maxLevel = _levels[^1];
        }

        public void AddCube(ICubeController cube)
        {
            var initLevel = _levelGeneration.Generate();
            
            var changerColor = cube.ChangerColor;
            changerColor.ApplyColor(initLevel);
            
            _cubeLevels.Add(cube, initLevel);

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
            var aLevel = _cubeLevels[a];
            var bLevel = _cubeLevels[b];

            if (aLevel == _maxLevel || bLevel == _maxLevel)
            {
                return;
            }
            
            if (aLevel != bLevel)
            {
                return;
            }
            
            var (main, sucker) = GetMainAndSuckerBalls(a, b);

            UpdateLevelForCube(main);
            
            var suckerGroup = GetOrCreateGroup(_cubeLevels[sucker]);
            sucker.RemoveFromGroup(suckerGroup);
            sucker.Destroy();
        }

        private void UpdateLevelForCube(ICubeController cube)
        {
            var level = _cubeLevels[cube];
            var oldGroup = GetOrCreateGroup(level);
            cube.RemoveFromGroup(oldGroup);
            
            var nextLevel = GetNextLevel(level);
            _cubeLevels[cube] = nextLevel;
            cube.ChangerColor.ApplyColor(nextLevel);
            var newGroup = GetOrCreateGroup(nextLevel);
            cube.AddToGroup(newGroup);
        }

        private CubeLevel GetNextLevel(CubeLevel a)
        {
            var levelIndex = (int)a;
            var nextLevel = _levels[Math.Min(_levels.Length - 1, levelIndex + 1)];
            
            return nextLevel;
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