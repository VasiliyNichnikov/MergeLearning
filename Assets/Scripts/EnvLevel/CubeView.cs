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
        
        [SerializeField]
        private CubeTrigger _trigger = null!;

        public event ICubeController.TriggerWithOtherCube? OnTriggerWithOtherCube;

        public bool IsSelected { get; private set; }
        
        public ChangerColor ChangerColor { get; private set; } = null!;

        public void Awake()
        {
            var material = _renderer.material;
            ChangerColor = new ChangerColor(material);
            _trigger.Init(CallOnTriggerWithOtherCube);
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

        public void AddToGroup(CubeGroupCollider group) => group.AddToGroup(_boxCollider);

        public void RemoveFromGroup(CubeGroupCollider group) => group.RemoveFromGroup(_boxCollider);
        
        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void CallOnTriggerWithOtherCube(ICubeController otherCube) => OnTriggerWithOtherCube?.Invoke(otherCube);
    }
}