#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace MergeLogic
{
    public class CubeGroupCollider
    {
        private readonly List<Collider> _colliders = new List<Collider>();
        
        public void AddToGroup(Collider collider)
        {
            foreach (var item in _colliders)
            {
                Physics.IgnoreCollision(item, collider, true);
            }

            _colliders.Add(collider);
        }

        public void RemoveFromGroup(Collider collider)
        {
            _colliders.Remove(collider);
            
            foreach (var item in _colliders)
            {
                Physics.IgnoreCollision(item, collider, false);
            }
        }
    }
}