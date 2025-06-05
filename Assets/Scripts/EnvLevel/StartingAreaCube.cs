#nullable enable

using System;
using System.Collections;
using UnityEngine;

namespace EnvLevel
{
    public class StartingAreaCube : MonoBehaviour
    {
        public event Action? OnRelease;
        
        [SerializeField]
        private BoxCollider _boxCollider = null!;
        
        public Vector3 Position => transform.position;

        private CubeView? _selectedCube;
        
        private void OnTriggerEnter(Collider other)
        {
            var cube = other.GetComponent<CubeView>();

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
                yield return null;

                if (!_boxCollider.bounds.Intersects(_selectedCube.Bounds))
                {
                    _selectedCube = null;
                }
            }
            
            OnRelease?.Invoke();
        }
    }
}