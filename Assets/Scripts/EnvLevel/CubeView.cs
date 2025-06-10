#nullable enable

using System;
using MergeLogic;
using UnityEngine;

namespace EnvLevel
{
    public class CubeView : MonoBehaviour, ICubeController
    {
        public Bounds Bounds => _boxCollider.bounds;
        
        public event ICubeController.TryMerge? OnTryMerge;
        
        public event Action? OnBeforeDestroy;

        [SerializeField]
        private Rigidbody _rigidbody = null!;

        [SerializeField]
        private BoxCollider _boxCollider = null!;

        [SerializeField]
        private MeshRenderer _renderer = null!;

        [SerializeField]
        private CubeTrigger _cubeTrigger = null!;

        public bool IsSelected { get; private set; }
        
        public float SpeedOfMovement => _rigidbody.linearVelocity.magnitude;

        public Vector3 Position => transform.position;

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
            IsSelected = true;
        }

        public void Deselect()
        {
            _rigidbody.isKinematic = false;
            IsSelected = false;
        }

        public void Destroy()
        {
            OnBeforeDestroy?.Invoke();
            
            Destroy(gameObject);
            Destroy(this);
        }

        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Force);
        }

        public void AddToGroup(CollisionGroup group)
        {
            group.Add(_boxCollider);
        }

        public void RemoveFromGroup(CollisionGroup group)
        {
            group.Remove(_boxCollider);
        }

        private void OnTryMergeCall(ICubeController otherBall)
        {
            OnTryMerge?.Invoke(this, otherBall);
        }
    }
}