using UnityEngine;

namespace Zparta.Pathfinding
{
    public interface IPathfinderSystem
    {
        public void LoadAndSetLevelGraph(TextAsset cache, Vector3 centerPoint);
    }
}