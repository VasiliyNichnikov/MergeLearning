#nullable enable

using System;
using UnityEngine;

namespace EnvLevel
{
    public class CubeTrigger : MonoBehaviour
    {
        private Action<ICubeController> _tryMergeAction = null!;
        
        public void Init(Action<ICubeController> tryMergeAction)
        {
            _tryMergeAction = tryMergeAction;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var otherBall = other.GetComponent<CubeView>();

            if (otherBall == null)
            {
                return;
            }
            
            _tryMergeAction.Invoke(otherBall);
        }
    }
}