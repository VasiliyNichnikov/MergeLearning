#nullable enable

using MergeLogic;

namespace EnvLevel
{
    public interface ICubeController
    {
        delegate void TriggerWithOtherCube(ICubeController otherCube);
        
        event TriggerWithOtherCube? OnTriggerWithOtherCube;
        
        bool IsSelected { get; }
        
        ChangerColor ChangerColor { get; }

        void AddToGroup(CubeGroupCollider group);

        void RemoveFromGroup(CubeGroupCollider group);

        void DestroySelf();
    }
}