#nullable enable

using System;
using UnityEngine;

namespace ScreenInteractions
{
    public interface IClickHandler : IDisposable
    {
        int MouseButton { get; }
        
        void Down(RaycastHit? hit);

        void Drag(RaycastHit? hit);
        
        void Up(RaycastHit? hit);
    }
}