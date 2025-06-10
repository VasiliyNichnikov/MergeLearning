#nullable enable

using System.Collections.Generic;
using Data;
using EnvLevel;
using UnityEngine;

namespace Magnet
{
    public class MagnetManager
    {
        private const float MinDistanceMagnetism = 1.5f;
        private const float AttractionForce = 50f;

        private readonly List<ICubeController> _cubes = new ();
        private readonly ICubeLevelsReadOnly _storage;
        
        public MagnetManager(ICubeLevelsReadOnly storage)
        {
            _storage = storage;
        }
        
        public void AddCube(ICubeController cube)
        {
            cube.OnBeforeDestroy += () => _cubes.Remove(cube);
            _cubes.Add(cube);
        }

        public void FixedUpdate()
        {
            for (var i = 0; i < _cubes.Count; i++)
            {
                for (var j = i + 1; j < _cubes.Count; j++)
                {
                    var firstCube = _cubes[i];
                    var secondCube = _cubes[j];
                    
                    var distance = Vector3.Distance(firstCube.Position, secondCube.Position);
                    
                    if (_storage.CanMergeByLevel(firstCube, secondCube) && distance < MinDistanceMagnetism)
                    {
                        if (firstCube.IsSelected)
                        {
                            Connect(secondCube, firstCube);
                        }
                        else if (secondCube.IsSelected)
                        {
                            Connect(firstCube, secondCube);
                        }
                        else
                        {
                            Connect(secondCube, firstCube);
                            Connect(firstCube, secondCube);
                        }
                    }
                }
            }
        }

        private static void Connect(ICubeController from, ICubeController to)
        {
            var direction = (to.Position - from.Position).normalized;
            from.AddForce(direction * AttractionForce);
        }
    }
}