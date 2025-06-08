#nullable enable

namespace EnvLevel
{
    public interface ICubeController
    {
        delegate void TryMerge(ICubeController a, ICubeController b);
        
        event TryMerge? OnTryMerge;
        
        float SpeedOfMovement { get; }
        
        ChangerColor ChangerColor { get; }

        void Destroy();
    }
}