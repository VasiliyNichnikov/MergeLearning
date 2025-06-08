#nullable enable

using Data;
using UnityEngine;

namespace EnvLevel
{
    public class ChangerColor
    {
        private readonly Material _material;
        
        public ChangerColor(Material material)
        {
            _material = material;
        }

        public void ApplyColor(CubeLevel level)
        {
            var colors = Main.Instance.GeneralConfig.CubeColorsConfig.GetData();
            _material.color = colors.GetColor(level);
        }
    }
}