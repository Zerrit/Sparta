using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zparta.EnemiesLogic;
using Zparta.Levels.EnemySpawn;
using Zparta.Levels.PickableObjects;
using Zparta.Levels.Traps;
using Zparta.Pathfinding;

namespace Zparta.Levels.Stages
{
    public abstract class AbstractStage : MonoBehaviour
    {
        public event Action OnStageComplete;
        public event Action<int> OnEnemiesSpawned;
        public event Action<int> OnEnemyKilled;
        public event Action OnAllEnemiesDefeated;
        public event Action OnCoinPicked;
        public event Action OnPowerUpPicked;

        [SerializeField] public StartBridge startBridge;
        [SerializeField] public FinishBridge finishBridge;
        [SerializeField] private SpawnGroup[] spawnGroups;
        [SerializeField] private AbstractPowerUp[] _powerUps;
        [SerializeField] private AbstractInteraction[] _interactions;
        [SerializeField] private Coin[] coins;
        [SerializeField] private TextAsset graphCache;
        [SerializeField] private Transform centerGraphPoint;
        [SerializeField] private ParticleSystem[] _victoryFX;

        protected int _remainingEnemies;
        protected List<IEnemy> _enemiesOnStage = new ();

        private IEnemySpawner _spawner;
        private IPathfinderSystem _pathfinderSystem;


        [Inject]
        public void Construct(IEnemySpawner spawner, IPathfinderSystem pathfinderSystem)
        {
            _spawner = spawner;
            _pathfinderSystem = pathfinderSystem;

            ResetStage();
            
            foreach (AbstractPowerUp powerUp in _powerUps)
                powerUp.OnPicked += () => OnPowerUpPicked?.Invoke();
            
            /*foreach (AbstractInteraction interaction in _interactions)
                interaction.OnPicked += () => OnPowerUpPicked?.Invoke();*/
            
            foreach (Coin coin in coins)
                coin.OnPicked += () => OnCoinPicked?.Invoke();
        }


        /// <summary>
        /// Приводит в действие уровень и все объекты на нём.
        /// </summary>
        public virtual void Activate()
        {
            _pathfinderSystem.LoadAndSetLevelGraph(graphCache, centerGraphPoint.position);
            SpawnEnemies();
            startBridge.HideBarrier();

            finishBridge.OnPlayerFinished += CompleteStage;
        }

        /// <summary>
        /// Отключает все сущности на уровне и удаляет оставшиеся объекты.
        /// </summary>
        public virtual void Deactivate()
        {
            CleareStage();
            finishBridge.OnPlayerFinished -= CompleteStage;
        }

        public void ActivateEnemies()
        {
            foreach (var enemy in _enemiesOnStage)
            {
                enemy.Activate();
            }
        }
        
        /// <summary>
        /// Возвращает уровень к изначальному состоянию.
        /// </summary>
        public virtual void ResetStage()
        {
            startBridge.ShowBarrier();
            finishBridge.HideBarrier();
            
            foreach (Coin coin in coins)
                coin.gameObject.SetActive(true);
            
            foreach (AbstractPowerUp powerUp in _powerUps)
                powerUp.gameObject.SetActive(true);
            
            foreach (AbstractInteraction interaction in _interactions)
                interaction.gameObject.SetActive(true);
            
            //Возврат усилений и перективация ловушек.
        }

        public virtual void EnableFireWorks()
        {
            foreach (var fx in _victoryFX)
            {
                fx.Play();
            }
        }
        
        /// <summary>
        /// Метод вызываемый при выполнении всех условий для прохождения этапа.
        /// </summary>
        protected virtual void CompleteStage()
        {
            Deactivate();
            OnStageComplete?.Invoke();
        }
        
        /// <summary>
        /// Снимает с учёта сброшенных врагов.
        /// </summary>
        /// <param name="enemy"> Ссылка на сброшенного врага. </param>
        protected virtual void ReduceRemainingEnemies(IEnemy enemy)
        {
            _remainingEnemies--;
            enemy.OnKilled -= ReduceRemainingEnemies;
            _enemiesOnStage.Remove(enemy);
            OnEnemyKilled?.Invoke(enemy.RewardValue);
            
            if (_remainingEnemies == 0)
                OnAllEnemiesDefeated?.Invoke();
        }
        
        /// <summary>
        /// Спавнит врагов на уровне согласно данным имеющихся SpawnPoints внутри SpawnGroups.
        /// </summary>
        protected void SpawnEnemies()
        {
            foreach (SpawnGroup group in  spawnGroups)
            {
                foreach (SpawnPoint point in group.spawnPoints)
                {
                    IEnemy unit = _spawner.Spawn(point.enemyId, point.Position);
                    unit.OnKilled += ReduceRemainingEnemies;
                    _enemiesOnStage.Add(unit);
                    _remainingEnemies++;
                }
            }
            OnEnemiesSpawned?.Invoke(_remainingEnemies);
        }

        /// <summary>
        /// Убирает с уровня все оставшиеся элементы.
        /// </summary>
        protected void CleareStage()
        {
            Debug.LogWarning("Запущена очистка уровня");
            foreach (var enemy in _enemiesOnStage)
            {
                enemy.OnKilled -= ReduceRemainingEnemies;
                enemy.Kill();
            }
            _enemiesOnStage.Clear();
            _remainingEnemies = 0;
        }
    }
}