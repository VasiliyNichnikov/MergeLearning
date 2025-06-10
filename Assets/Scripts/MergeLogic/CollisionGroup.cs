#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace MergeLogic
{
    public class CollisionGroup
    {
        private readonly List<Collider> _colliders = new List<Collider>();
        
        public void Add(Collider newCollider)
        {
            foreach (var collider in _colliders)
            {
                Physics.IgnoreCollision(newCollider, collider, true);
            }
            
            _colliders.Add(newCollider);
        }

        public void Remove(Collider colliderForRemove)
        {
            foreach (var collider in _colliders)
            {
                if (Equals(colliderForRemove, collider))
                {
                    continue;
                }
                
                Physics.IgnoreCollision(colliderForRemove, collider, false);
            }
            
            _colliders.Remove(colliderForRemove);
        }
    }
}