#nullable enable

using UnityEngine;

namespace EnvLevel
{
    public class EnvData : MonoBehaviour
    {
        public EnvObject LeftWall => _leftWall;
        
        public EnvObject RightWall => _rightWall;
        
        public EnvObject Ground => _ground;
        
        [SerializeField] 
        private EnvObject _leftWall = null!;
        
        [SerializeField]
        private EnvObject _rightWall = null!;
        
        [SerializeField]
        private EnvObject _ground = null!;
    }
}