#nullable enable

using MergeLogic;

namespace EnvLevel
{
    public interface ICubeController
    {
        delegate void TryMerge(ICubeController a, ICubeController b);
        
        event TryMerge? OnTryMerge;
        
        float SpeedOfMovement { get; }
        
        ChangerColor ChangerColor { get; }

        void AddToGroup(CollisionGroup group);
        
        void RemoveFromGroup(CollisionGroup group);

        void Destroy();
    }
}