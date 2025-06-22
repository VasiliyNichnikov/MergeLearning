using Data;

namespace MergeLogic
{
    public class MergeManager
    {
        private readonly CubeSceneStorage _cubeStorage;
        
        public MergeManager(CubeSceneStorage cubeStorage)
        {
            _cubeStorage = cubeStorage;
        }
    }
}