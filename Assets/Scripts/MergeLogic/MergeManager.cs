using System;
using System.Collections.Generic;
using Data;
using EnvLevel;

namespace MergeLogic
{
    public class MergeManager
    {
        private readonly Dictionary<ICubeController, CubeLevel> _cubeLevels = new();
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
            
            cube.OnTryMerge += OnTryMerge;
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
            
            var nextLevel = GetNextLevel(aLevel);
            var (main, sucker) = GetMainAndSuckerBalls(a, b);

            _cubeLevels[main] = nextLevel;
            main.ChangerColor.ApplyColor(nextLevel);
            
            sucker.Destroy();
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