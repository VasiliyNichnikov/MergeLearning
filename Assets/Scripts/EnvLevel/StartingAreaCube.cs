#nullable enable

using System;
using System.Collections;
using UnityEngine;

namespace EnvLevel
{
    public class StartingAreaCube : MonoBehaviour
    {
        public event Action? OnRelease;

        private const float DelayBeforeCreatingCube = 0.25f;
        
        [SerializeField]
        private BoxCollider _boxCollider = null!;
        
        public Vector3 Position => transform.position;

        private CubeView? _selectedCube;
        
        private void OnTriggerEnter(Collider other)
        {
            var cube = other.GetComponent<CubeView>();

            if (cube == null)
            {
                return;
            }
            
            if (_selectedCube == null)
            {
                _selectedCube = cube;
                
                StartCoroutine(WaitingForCubeToExit());
            }
        }

        private IEnumerator WaitingForCubeToExit()
        {
            while (_selectedCube != null)
            {
                if (!_boxCollider.bounds.Intersects(_selectedCube.Bounds))
                {
                    _selectedCube = null;
                }
                
                yield return null;
            }

            yield return new WaitForSeconds(DelayBeforeCreatingCube);
            
            OnRelease?.Invoke();
        }
    }
}