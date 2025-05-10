#nullable enable

using System;
using EnvLevel;
using ScreenInteractions;
using UnityEngine;

namespace Locations
{
    public class GameLocation : IDisposable
    {
        private readonly CubeClickHandler _cubeClickHandler;
        
        public GameLocation(Camera camera, EnvData envData)
        {
            var cubePositionInspector = new CubePositionInspector(envData);
            _cubeClickHandler = new CubeClickHandler(camera, cubePositionInspector);
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