#nullable enable

using System;
using Data;
using EnvLevel;
using Factories;
using MergeLogic;
using ScreenInteractions;
using UnityEngine;

namespace Locations
{
    public class GameLocation : IDisposable
    {
        private readonly CubeClickHandler _cubeClickHandler;
        private readonly CubeSpawner _spawner;
        private readonly CubeView _cubeViewPrefab;
        private readonly CubeSceneStorage _sceneStorage;
        
        public GameLocation(
            Camera camera, 
            EnvData envData, 
            CubeSpawner spawner,
            CubeView cubeViewPrefab,
            CubeSceneStorage sceneStorage)
        {
            var cubePositionInspector = new CubePositionInspector(envData);
            _cubeClickHandler = new CubeClickHandler(camera, cubePositionInspector);
            _spawner = spawner;
            _cubeViewPrefab = cubeViewPrefab;
            _sceneStorage = sceneStorage;
        }

        public void Load()
        {
            var cubeViewFactory = new CubeViewFactory(_sceneStorage, new LevelGeneration(), _cubeViewPrefab);
            
            _spawner.Init(cubeViewFactory);
            Main.Instance.ClickManager.AddHandler(_cubeClickHandler);
        }

        public void Dispose()
        {
            Main.Instance.ClickManager.RemoveHandler(_cubeClickHandler);
        }
    }
}