using EnvLevel;

namespace MergeLogic
{
    public class MergeManager
    {
        private readonly LevelGeneration _levelGeneration;
        
        public MergeManager(LevelGeneration levelGeneration)
        {
            _levelGeneration = levelGeneration;
        }

        public void AddCube(ICubeController cube)
        {
            var initLevel = _levelGeneration.Generate();
            
            var changerColor = cube.ChangerColor;
            changerColor.ApplyColor(initLevel);
        }
    }
}