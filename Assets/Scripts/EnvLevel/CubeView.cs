#nullable enable

using MergeLogic;
using UnityEngine;

namespace EnvLevel
{
    public class CubeView : MonoBehaviour, ICubeController
    {
        public Bounds Bounds => _boxCollider.bounds;
        
        [SerializeField]
        private Rigidbody _rigidbody = null!;

        [SerializeField]
        private BoxCollider _boxCollider = null!;

        [SerializeField]
        private MeshRenderer _renderer = null!;

        public ChangerColor ChangerColor { get; private set; } = null!;

        public void Awake()
        {
            var material = _renderer.material;
            ChangerColor = new ChangerColor(material);
        }
        
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