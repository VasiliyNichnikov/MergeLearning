#nullable enable

using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GeneralConfig", menuName = "Configs/GeneralConfig", order = 0)]
    public class GeneralConfig : ScriptableObject
    {
        public EnvConfig EnvConfig => _envConfig;
        
        [SerializeField]
        private EnvConfig _envConfig = null!;
    }
}