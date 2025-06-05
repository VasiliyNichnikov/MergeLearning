#nullable enable

using UnityEngine;

namespace EnvLevel
{
    public class CubeView : MonoBehaviour
    {
        public Bounds Bounds => _boxCollider.bounds;
        
        [SerializeField]
        private Rigidbody _rigidbody = null!;

        [SerializeField]
        private BoxCollider _boxCollider = null!;
        
        public void Select()
        {
            _rigidbody.isKinematic = true;
        }

        public void Deselect()
        {
            _rigidbody.isKinematic = false;
        }
    }
}