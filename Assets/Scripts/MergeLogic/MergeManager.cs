using Data;
using EnvLevel;

namespace MergeLogic
{
    public class MergeManager
    {
        private readonly LevelGeneration _levelGeneration;
        private readonly CubeColorsData _cubeColorsData;
        
        public MergeManager(LevelGeneration levelGeneration, CubeColorsData cubeColorsData)
        {
            _levelGeneration = levelGeneration;
            _cubeColorsData = cubeColorsData;
        }

        public void AddCube(ICubeController cube)
        {
            var initLevel = _levelGeneration.Generate();
            var ballColor = _cubeColorsData.GetColor(initLevel);
            
            var changerColor = cube.ChangerColor;
            changerColor.ApplyColor(ballColor);
        }
    }
}