#nullable enable

using System;
using EnvLevel;
using Factories;
using ScreenInteractions;
using UnityEngine;

namespace Locations
{
    public class GameLocation : IDisposable
    {
        private readonly CubeClickHandler _cubeClickHandler;
        private readonly CubeSpawner _spawner;
        private readonly CubeView _cubeViewPrefab;
        
        public GameLocation(
            Camera camera, 
            EnvData envData, 
            CubeSpawner spawner,
            CubeView cubeViewPrefab)
        {
            var cubePositionInspector = new CubePositionInspector(envData);
            _cubeClickHandler = new CubeClickHandler(camera, cubePositionInspector);
            _spawner = spawner;
            _cubeViewPrefab = cubeViewPrefab;
        }

        public void Load()
        {
            var mergeManager = Main.Instance.MergeManager;
            var cubeViewFactory = new CubeViewFactory(mergeManager, _cubeViewPrefab);
            
            _spawner.Init(cubeViewFactory);
            Main.Instance.ClickManager.AddHandler(_cubeClickHandler);
        }

        public void Dispose()
        {
            Main.Instance.ClickManager.RemoveHandler(_cubeClickHandler);
        }
    }
}