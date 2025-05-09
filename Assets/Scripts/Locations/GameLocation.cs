#nullable enable

using System;
using ScreenInteractions;
using UnityEngine;

namespace Locations
{
    public class GameLocation : IDisposable
    {
        private readonly CubeClickHandler _cubeClickHandler;
        
        public GameLocation(Camera camera)
        {
            _cubeClickHandler = new CubeClickHandler(camera);
        }

        public void Load()
        {
            Main.Instance.ClickManager.AddHandler(_cubeClickHandler);
        }

        public void Dispose()
        {
            Main.Instance.ClickManager.RemoveHandler(_cubeClickHandler);
        }
    }
}