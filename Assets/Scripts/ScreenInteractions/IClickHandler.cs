#nullable enable

using UnityEngine;

namespace ScreenInteractions
{
    public interface IClickHandler
    {
        int MouseButton { get; }
        
        void Down(RaycastHit? hit);

        void Drag(RaycastHit? hit);
        
        void Up(RaycastHit? hit);
    }
}