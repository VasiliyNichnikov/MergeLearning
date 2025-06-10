#nullable enable

using System;
using MergeLogic;
using UnityEngine;

namespace EnvLevel
{
    public interface ICubeController
    {
        delegate void TryMerge(ICubeController a, ICubeController b);
        
        event TryMerge? OnTryMerge;

        event Action? OnBeforeDestroy; 
        
        bool IsSelected { get; }
        
        float SpeedOfMovement { get; }
        
        Vector3 Position { get; set; }
        
        ChangerColor ChangerColor { get; }

        void Select();
        
        void Deselect();
        
        void AddToGroup(CollisionGroup group);
        
        void RemoveFromGroup(CollisionGroup group);

        void AddForce(Vector3 force);
        
        void Destroy();
    }
}