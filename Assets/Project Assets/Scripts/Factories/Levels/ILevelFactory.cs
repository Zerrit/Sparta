using System.Collections.Generic;
using UnityEngine;
using Zparta.Levels.Stages;

namespace Zparta.Factories.Levels
{
    public interface ILevelFactory
    {
        public int GetLevelsCount();
        public int[] GetStarsTresholds(int levelId);
        
        public void Create(int levelId, Vector3 position, Transform parent, List<AbstractStage> levelStages);
    }
}