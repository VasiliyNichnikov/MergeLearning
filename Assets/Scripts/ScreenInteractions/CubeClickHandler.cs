#nullable enable

using EnvLevel;
using UnityEngine;

namespace ScreenInteractions
{
    public class CubeClickHandler : IClickHandler
    {
        public int MouseButton => LeftMouseButton;
        
        private const int LeftMouseButton = 0;
        
        private readonly Camera _camera;

        private float _cameraZDistance;
        private CubeView? _selectedCube;
        
        public CubeClickHandler(Camera camera)
        {
            _camera = camera;
        }

        public void Down(RaycastHit? hit)
        {
            if (hit == null)
            {
                return;
            }

            if (_selectedCube != null)
            {
                Debug.LogError("CubeClickHandler.Down: Selected cube already exists.");
                
                return;
            }
            
            var cubeView = hit.Value.transform.GetComponent<CubeView>();

            if (cubeView == null)
            {
                return;
            }

            _cameraZDistance = _camera.WorldToScreenPoint(cubeView.transform.position).z;
            _selectedCube = cubeView;
            _selectedCube.Select();
        }

        public void Drag(RaycastHit? hit)
        {
            if (_selectedCube == null)
            {
                return;
            }
            
            var mousePosition = Input.mousePosition;
            var screenPosition = new Vector3(mousePosition.x, mousePosition.y, _cameraZDistance);
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            _selectedCube.transform.position = worldPosition;
        }

        public void Up(RaycastHit? hit)
        {
            if (_selectedCube == null)
            {
                return;
            }
            
            _selectedCube.Deselect();
            _selectedCube = null;
        }
    }
}