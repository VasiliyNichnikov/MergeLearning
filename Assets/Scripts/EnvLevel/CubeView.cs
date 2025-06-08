#nullable enable

using UnityEngine;

namespace EnvLevel
{
    public class CubeView : MonoBehaviour, ICubeController
    {
        public Bounds Bounds => _boxCollider.bounds;
        
        public event ICubeController.TryMerge? OnTryMerge;
        
        [SerializeField]
        private Rigidbody _rigidbody = null!;

        [SerializeField]
        private BoxCollider _boxCollider = null!;

        [SerializeField]
        private MeshRenderer _renderer = null!;

        [SerializeField]
        private CubeTrigger _cubeTrigger = null!;
        
        public float SpeedOfMovement => _rigidbody.linearVelocity.magnitude;
        
        public ChangerColor ChangerColor { get; private set; } = null!;

        public void Awake()
        {
            var material = _renderer.material;
            ChangerColor = new ChangerColor(material);
            _cubeTrigger.Init(OnTryMergeCall);
        }
        
        public void Select()
        {
            _rigidbody.isKinematic = true;
        }

        public void Deselect()
        {
            _rigidbody.isKinematic = false;
        }
        
        public void Destroy()
        {
            Destroy(gameObject);
            Destroy(this);
        }

        private void OnTryMergeCall(ICubeController otherBall)
        {
            OnTryMerge?.Invoke(this, otherBall);
        }
    }
}