#nullable enable

using UnityEngine;

namespace EnvLevel
{
    public class EnvObject : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider _boxCollider = null!;

        /// <summary>
        /// 0 - top,
        /// 1 - right,
        /// 2 - left
        /// </summary>
        public Vector3[] Bounds => _bounds ??= CalculateBounds();
        
        private Vector3[]? _bounds;
        
        private Vector3[] CalculateBounds()
        {
            var bounds = _boxCollider.bounds;
            var center = bounds.center;

            var maxTopPosition = new Vector3(center.x, float.MaxValue, center.z);
            var maxRightPosition = new Vector3(float.MaxValue, center.y, center.z);
            var minLeftPosition = new Vector3(float.MinValue, center.y, center.z);

            var top = bounds.ClosestPoint(maxTopPosition);
            var right = bounds.ClosestPoint(maxRightPosition);
            var left = bounds.ClosestPoint(minLeftPosition);

            return new[]
            {
                top,
                right,
                left
            };
        }
    }
}