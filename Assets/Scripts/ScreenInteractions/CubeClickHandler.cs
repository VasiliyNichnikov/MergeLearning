#nullable enable

using System.Collections;
using EnvLevel;
using UnityEngine;

namespace ScreenInteractions
{
    public class CubeClickHandler : IClickHandler
    {
        public int MouseButton => LeftMouseButton;
        
        private const int LeftMouseButton = 0;
        
        private readonly Camera _camera;
        private readonly CubePositionInspector _cubePositionInspector;

        private ICubeController? _selectedCube;
        
        public CubeClickHandler(Camera camera, CubePositionInspector cubePositionInspector)
        {
            _camera = camera;
            _cubePositionInspector = cubePositionInspector;
            Main.Instance.MergeManager.OnSelectedCubeChanged += OnSelectedCubeChanged;
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

            SelectCube(cubeView);
        }

        public void Drag(RaycastHit? hit)
        {
            if (_selectedCube == null)
            {
                return;
            }
            
            var mousePosition = Input.mousePosition;
            var cameraZDistance = _camera.WorldToScreenPoint(_selectedCube.Position).z;
            var screenPosition = new Vector3(mousePosition.x, mousePosition.y, cameraZDistance);
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            var finalPosition = _cubePositionInspector.ValidatePosition(worldPosition);
            
            _selectedCube.Position = finalPosition;
        }

        public void Up(RaycastHit? hit) => DeselectCube();

        private void OnSelectedCubeChanged(ICubeController selectedCube)
        {
            DeselectCube();
            SelectCube(selectedCube);
        }

        private void SelectCube(ICubeController newCube)
        {
            _selectedCube = newCube;
            _selectedCube.Select();
            
            var cubePositionOnZAxis = Main.Instance.GeneralConfig.EnvConfig.CubePositionOnZAxis;
            
            if (!Mathf.Approximately(_selectedCube.Position.z, cubePositionOnZAxis))
            {
                Main.Instance.StartCoroutine(AnimationOfZAxisAdjustment());
            }
        }

        private void DeselectCube()
        {
            if (_selectedCube == null)
            {
                return;
            }
            
            _selectedCube.Deselect();
            _selectedCube = null;
        }
        
        private IEnumerator AnimationOfZAxisAdjustment()
        {
            const float maxProgress = 1.0f;
            
            var time = 0.0f;
            var progress = 0.0f;
            var envConfig = Main.Instance.GeneralConfig.EnvConfig;
            var timeAnimation = envConfig.AdjustingCubeAlongZAxis.Time;
            var cubePositionOnZAxis = envConfig.CubePositionOnZAxis;

            while (progress < maxProgress && _selectedCube != null)
            {
                progress = Mathf.Clamp01(time / timeAnimation);
                
                var cubePosition = _selectedCube.Position;
                var cubeEndPosition = new Vector3(cubePosition.x, cubePosition.y, cubePositionOnZAxis);
                
                var updatedCubePosition = Vector3.Lerp(cubePosition, cubeEndPosition, progress);

                _selectedCube.Position = updatedCubePosition;
                
                yield return null;
                
                time += Time.deltaTime;
            }
        }

        public void Dispose()
        {
            Main.Instance.MergeManager.OnSelectedCubeChanged -= OnSelectedCubeChanged;
        }
    }
}