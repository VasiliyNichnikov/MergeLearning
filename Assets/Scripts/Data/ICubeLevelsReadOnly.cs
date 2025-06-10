using EnvLevel;

namespace Data
{
    public interface ICubeLevelsReadOnly
    {
        bool CanMergeByLevel(ICubeController a, ICubeController b);
    }
}