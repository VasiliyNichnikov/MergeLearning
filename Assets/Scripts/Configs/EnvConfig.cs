#nullable enable

using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnvConfig", menuName = "Configs/EnvConfig", order = 0)]
    public class EnvConfig : ScriptableObject
    {
        [Serializable]
        public struct AdjustingCubeAlongZAxisData
        {
            [Min(0)]
            public float Time;
        }
        
        public AdjustingCubeAlongZAxisData AdjustingCubeAlongZAxis => _adjustingCubeAlongZAxis;
        
        public float CubePositionOnZAxis => _cubePositionOnZAxis;
        
        [SerializeField] 
        private float _cubePositionOnZAxis;
        
        [SerializeField]
        private AdjustingCubeAlongZAxisData _adjustingCubeAlongZAxis;
    }
}