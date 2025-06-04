#nullable enable

using UnityEngine;

namespace EnvLevel
{
    public class CubeJellyDeformer : MonoBehaviour
    {
        private class JellyVertex
        {
            private const float MinValue = 0.001f;
            
            public Vector3 Position { get; private set; }

            private Vector3 _force;
            private Vector3 _velocity;
            
            public JellyVertex(Vector3 position)
            {
                Position = position;
            }

            public void Snake(Vector3 targetPosition, float stiffness, float damping)
            {
                _force = (targetPosition - Position) * stiffness;
                _velocity = (_velocity + _force / 1) * damping;
                Position += _velocity;

                if ((_velocity + _force + _force / 2).magnitude < MinValue)
                {
                    Position = targetPosition;
                }
            }
        }
        
        [SerializeField]
        private float _intensity;
        
        [SerializeField]
        private float _stiffness;
        
        [SerializeField]
        private float _damping;
        
        [SerializeField]
        private MeshFilter _meshFilter = null!;

        [SerializeField]
        private MeshRenderer _meshRenderer = null!;

        private JellyVertex[] _jv = null!;
        
        private Mesh _originalMesh = null!;
        private Mesh _cloneMesh = null!;
        
        private void Start()
        {
            _originalMesh = _meshFilter.sharedMesh;
            _cloneMesh = Instantiate(_originalMesh);
            _meshFilter.sharedMesh = _cloneMesh;
         
            _jv = new JellyVertex[_cloneMesh.vertexCount];
            
            for (var i = 0; i < _jv.Length; i++)
            {
                _jv[i] = new JellyVertex(transform.TransformPoint(_cloneMesh.vertices[i]));
            }
        }

        private void FixedUpdate()
        {
            var vertexArray = _originalMesh.vertices;

            for (var i = 0; i < vertexArray.Length; i++)
            {
                var target = transform.TransformPoint(vertexArray[i]);
                var bounds = _meshRenderer.bounds;
                var intensity = (1 - (bounds.max.y - target.y) / bounds.size.y) * _intensity;
                _jv[i].Snake(target, _stiffness, _damping);
                target = transform.InverseTransformPoint(_jv[i].Position);
                vertexArray[i] = Vector3.Lerp(vertexArray[i], target, intensity);
            }

            _cloneMesh.vertices = vertexArray;
            _cloneMesh.RecalculateNormals();
        }
    }
}