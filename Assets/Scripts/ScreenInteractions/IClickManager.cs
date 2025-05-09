#nullable enable

namespace ScreenInteractions
{
    public interface IClickManager
    {
        void AddHandler(IClickHandler handler);
        
        void RemoveHandler(IClickHandler handler);
    }
}