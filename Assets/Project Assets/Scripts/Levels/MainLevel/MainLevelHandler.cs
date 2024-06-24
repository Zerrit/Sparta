using System;
using System.Collections.Generic;
using UnityEngine;
using Zparta.Factories.Levels;
using Zparta.Levels.Model;
using Zparta.Levels.Stages;
using Zparta.PlayerLogic.Model;
using Object = UnityEngine.Object;

namespace Zparta.Levels.MainLevel
{
    public class MainLevelHandler : IMainLevelHandler
    {
        public event Action OnMainLevelComplete;
        
        public AbstractStage ActiveStage => _levelStages[_levelModel.CurrentStage];

        private readonly List<AbstractStage> _levelStages = new();
        private readonly Transform _levelContainer;
        private readonly ILevelModel _levelModel;
        private readonly ILevelFactory _factory;
        private readonly IPlayerModel _playerModel;

        private int _activeLevel;


        public MainLevelHandler(Transform levelContainer, ILevelModel levelModel, ILevelFactory factory, IPlayerModel playerModel)
        {
            _factory = factory;
            _playerModel = playerModel;
            _levelModel = levelModel;

            _levelContainer = levelContainer;
            _levelModel.LevelCount = _factory.GetLevelsCount();
            _activeLevel = -1;
        }


        public void InitializeLevel()
        {
            if (_levelModel.CurrentLevel != _activeLevel)
            {
                RemoveLevel();
                _activeLevel = _levelModel.CurrentLevel;
                CreateLevel();
            }
            else
                RestartLevel();

            _levelModel.ClearTempData();
            _levelModel.CurrentStage = 0;
            ActivateCurrentStage();
            _playerModel.PlacePlayer(new Vector3(0, 0, 1.5f));
        }

        void IMainLevelHandler.InitializeLevel(int levelId)
        {
            _levelModel.CurrentLevel = levelId;
            
            if (_levelModel.CurrentLevel != levelId)
            {
                RemoveLevel();
                _activeLevel = levelId;
                CreateLevel();
            }
            else
                RestartLevel();

            _levelModel.ClearTempData();
            _levelModel.CurrentStage = 0;
            ActivateCurrentStage();
            
            _playerModel.PlacePlayer(new Vector3(0, 0, 1.5f));
        }

        void IMainLevelHandler.InitializeCurrentStage()
        {
            _playerModel.PlacePlayer(ActiveStage.startBridge.transform.position);
        }
        
        void IMainLevelHandler.ActivateEnamies()
        {
            _levelStages[_levelModel.CurrentStage].ActivateEnemies();
        }
        
        public void RemoveLevel()
        {
            _activeLevel = -1;
            if(_levelStages.Count == 0) return;
            
            DeactivateCurrentStage();
            foreach (var stage in _levelStages)
            {
                Object.Destroy(stage.gameObject);
            }
            _levelStages.Clear();
        }

        /// <summary>
        /// Создаёт указанный уровень на сцене.
        /// </summary>
        private void CreateLevel()
        {
            _factory.Create(_levelModel.CurrentLevel, Vector3.zero, _levelContainer, _levelStages);
            _levelModel.StageCount = _levelStages.Count;
            _levelModel.StarsTresholds = _factory.GetStarsTresholds(_levelModel.CurrentLevel);
        }

        /// <summary>
        /// Перезапускает текущий уровень.
        /// </summary>
        private void RestartLevel()
        {
            Debug.LogWarning("Уровень перезапущен");
            DeactivateCurrentStage();

            foreach (AbstractStage stage in _levelStages)
                stage.ResetStage();
        }

        /// <summary>
        /// Активирует текущий этап уровня.
        /// </summary>
        private void ActivateCurrentStage()
        {
            ActiveStage.OnEnemiesSpawned += _levelModel.HandleSpawnedEnemies;
            ActiveStage.OnEnemyKilled += _levelModel.HandleKilledEnemy;
            ActiveStage.OnAllEnemiesDefeated += _levelModel.HandleClearedStage;
            ActiveStage.OnCoinPicked += _levelModel.HandlePickedCoin;
            ActiveStage.OnPowerUpPicked += _levelModel.HandlePickedPowerUp;
            ActiveStage.OnStageComplete += CompleteStage;
            
            ActiveStage.Activate();
        }
        
        /// <summary>
        /// Деактивирует текущий этап уровня.
        /// </summary>
        private void DeactivateCurrentStage()
        {
            ActiveStage.OnEnemiesSpawned -= _levelModel.HandleSpawnedEnemies;
            ActiveStage.OnEnemyKilled -= _levelModel.HandleKilledEnemy;
            ActiveStage.OnAllEnemiesDefeated -= _levelModel.HandleClearedStage;
            ActiveStage.OnCoinPicked -= _levelModel.HandlePickedCoin;
            ActiveStage.OnPowerUpPicked -= _levelModel.HandlePickedPowerUp;
            ActiveStage.OnStageComplete -= CompleteStage;
            
            ActiveStage.Deactivate();
        }
        
        /// <summary>
        /// Метод деактивирует пройденный этап и запускает следующий.
        /// </summary>
        private void CompleteStage()
        {
            Debug.Log($"Пройден этап {_levelModel.CurrentStage + 1}");

            DeactivateCurrentStage();

            if (!IsLastStage())
            {
                _levelModel.CurrentStage++;
                ActivateCurrentStage();
                _levelStages[_levelModel.CurrentStage].ActivateEnemies();
            }
            else
            {
                OnMainLevelComplete?.Invoke();
                _levelModel.CurrentLevel++;
                //_playerModel.ChangePlayerMode(PlayerMode.Celebrating);
                ActiveStage.EnableFireWorks();

                Debug.Log("Уровень пройден");
            }
        }

        /// <summary>
        /// Проверяет является ли текущий этап последним.
        /// </summary>
        private bool IsLastStage() 
            => _levelModel.CurrentStage + 1 == _levelModel.StageCount;
    }
}