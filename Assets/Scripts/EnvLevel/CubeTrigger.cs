#nullable enable

using UnityEngine;

namespace EnvLevel
{
    public class CubeTrigger : MonoBehaviour
    {
        private ICubeController.TriggerWithOtherCube _onTriggerWithOtherCube = null!;
        
        public void Init(ICubeController.TriggerWithOtherCube onTriggerWithOtherCube)
        {
            _onTriggerWithOtherCube = onTriggerWithOtherCube;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var cube = other.GetComponent<ICubeController>();

            if (cube == null)
            {
                return;
            }

            _onTriggerWithOtherCube.Invoke(cube);
        }
    }
}