#nullable enable

using EnvLevel;
using UnityEngine;

namespace ScreenInteractions
{
    public class CubePositionInspector
    {
        private const float DistanceFromCenterToEdgeOfCube = 0.5f;
        
        private readonly EnvData _envData;

        private float LeftBorderByAxisX => _envData.LeftWall.Bounds[1].x + DistanceFromCenterToEdgeOfCube;

        private float RightBorderByAxisX => _envData.RightWall.Bounds[2].x - DistanceFromCenterToEdgeOfCube;
        
        private float BottomBorderByAxisY => _envData.Ground.Bounds[0].y + DistanceFromCenterToEdgeOfCube;
        
        public CubePositionInspector(EnvData envData)
        {
            _envData = envData;
        }
        
        public Vector3 ValidatePosition(Vector3 position)
        {
            var x = position.x;
            var y = position.y;
            var z = position.z;

            if (x < LeftBorderByAxisX)
            {
                x = LeftBorderByAxisX;
            }

            if (x > RightBorderByAxisX)
            {
                x = RightBorderByAxisX;
            }

            if (y < BottomBorderByAxisY)
            {
                y = BottomBorderByAxisY;
            }
            
            return new Vector3(x, y, z);
        }
    }
}