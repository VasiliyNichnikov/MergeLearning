#nullable enable

using Factories;
using UnityEngine;

namespace EnvLevel
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField]
        private StartingAreaCube[] _startingAreas = null!;
        
        public void Init(CubeViewFactory cubeViewFactory)
        {
            for (var i = 0; i < _startingAreas.Length; i++)
            {
                var area = _startingAreas[i];
                cubeViewFactory.Create(area.Position, Quaternion.identity);

                area.OnRelease += () =>
                {
                    cubeViewFactory.Create(area.Position, Quaternion.identity);
                };
            }
        }
    }
}