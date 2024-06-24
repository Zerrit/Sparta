using System.Collections;
using Pathfinding;
using UnityEngine;

namespace Zparta.Pathfinding
{
    public class PathfinderSystem :MonoBehaviour, IPathfinderSystem
    {
        [SerializeField] private AstarPath aStar;

        private NavGraph _currentGraph;
        
        public void LoadAndSetLevelGraph(TextAsset cache, Vector3 centerPoint)
        {
            if(_currentGraph != null)
                aStar.data.RemoveGraph(aStar.data.graphs[0]);
            
            aStar.data.file_cachedStartup = cache;
            aStar.data.LoadFromCache();
            _currentGraph = aStar.data.graphs[0];
            
            aStar.data.gridGraph.center = centerPoint;
            StartCoroutine(Delay());


            Debug.LogWarning("Отсканирована новая карта");
        }

        private IEnumerator Delay()
        {
            yield return null;
            aStar.data.gridGraph.Scan();
        }
    }
}