#nullable enable

using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GeneralConfig", menuName = "Configs/GeneralConfig", order = 0)]
    public class GeneralConfig : ScriptableObject
    {
        public EnvConfig EnvConfig => _envConfig;
        
        public CubeColorsConfig CubeColorsConfig => _cubeColorsConfig;
        
        [SerializeField]
        private EnvConfig _envConfig = null!;
        
        [SerializeField]
        private CubeColorsConfig _cubeColorsConfig = null!;
    }
}