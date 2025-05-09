#nullable enable

using UnityEngine;

namespace EnvLevel
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody = null!;

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