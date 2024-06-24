using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zparta.Configs.Levels;
using Zparta.Levels.Stages;
using Zparta.Services;

namespace Zparta.Factories.Levels
{
    public class LevelFactory : ILevelFactory
    {
        private readonly DiContainer _container;
        private readonly LevelFactoryConfig _config;
        

        public LevelFactory(DiContainer container, LevelFactoryConfig config)
        {
            _container = container;
            _config = config;
        }


        public int GetLevelsCount()
            => _config.Levels.Length;

        public int[] GetStarsTresholds(int levelId)
        {
            return _config.Levels[levelId].ScoreTreshholds;
        }


        public void Create(int levelId, Vector3 position, Transform parent, List<AbstractStage> levelStages)
        {
            Vector3 lastStageSnapPoint = position;

            foreach (AbstractStage stagePrefab in _config.Levels[levelId].StagesQueue)
            {
                AbstractStage stage = _container.InstantiatePrefabForComponent<AbstractStage>(stagePrefab, lastStageSnapPoint, Quaternion.identity, parent);
                lastStageSnapPoint = stage.finishBridge.EndPoint.position;
                levelStages.Add(stage);
            }
        }
    }
}