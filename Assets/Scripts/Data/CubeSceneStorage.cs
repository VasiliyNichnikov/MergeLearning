#nullable enable

using System.Collections.Generic;
using EnvLevel;

namespace Data
{
    public class CubeSceneStorage
    {
        private readonly Dictionary<ICubeController, CubeLevel> _cubes = new ();

        public void AddCube(ICubeController cube, CubeLevel level)
        {
            _cubes.Add(cube, level);
        }
    }
}