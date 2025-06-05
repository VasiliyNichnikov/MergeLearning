#nullable enable

using UnityEngine;

namespace EnvLevel
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField]
        private StartingAreaCube[] _startingAreas = null!;

        [SerializeField]
        private CubeView _cubeView = null!;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            for (var i = 0; i < _startingAreas.Length; i++)
            {
                var area = _startingAreas[i];
                CreateCube(area.Position, Quaternion.identity);

                area.OnRelease += () =>
                {
                    CreateCube(area.Position, Quaternion.identity);
                };
            }
        }
        
        private CubeView CreateCube(Vector3 position, Quaternion rotation)
        {
            var createdCube = Instantiate(_cubeView, position, rotation);

            return createdCube;
        }
    }
}