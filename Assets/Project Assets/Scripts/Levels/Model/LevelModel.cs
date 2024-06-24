using System;
using UnityEngine;
using Zparta.ScoreLogic;
using Zparta.WalletLogic;

namespace Zparta.Levels.Model
{
    public class LevelModel : IReadOnlyLevelModel, ILevelModel
    {
        public event Action<int> OnLevelChanged;
        public event Action OnEnemyPushed;
        public event Action OnCoinPicked;
        public event Action OnPowerUpPicked;
        public event Action OnStageCleared;
        public event Action<int> OnStageFinished;
        
        public int LevelCount { get; set; }
        public int CurrentLevel { get; set; }
        public int StageCount { get; set; }
        public int CurrentStage
        {
            get => _currentStage;
            set
            {
                _currentStage = value;
                OnStageFinished?.Invoke(value);
            }
        }

        public int[] StarsTresholds { get; set; }

        public int Coins { get; private set; }
        public int Score => _scoreListener.Score;
        public int PushedEnemiesPercent
        {
            get
            {
                if (TotalEnemiesNumber <= 0)
                {
                    Debug.LogWarning("Попытка получить процент скинутых врагов при их полном отсутствии.");
                    return 0;
                }
                return PushedEnemies * 100 / TotalEnemiesNumber;
            }
        }

        public int PushedEnemies { get; private set; }
        public int TotalEnemiesNumber { get; private set; }

        private int _currentStage;
        
        private readonly IRewardable _wallet;
        private readonly IScoreChanger _scoreChanger;
        private readonly IScoreListener _scoreListener;

        public LevelModel(IRewardable wallet, IScoreChanger scroreChanger, IScoreListener scoreListener)
        {
            _wallet = wallet;
            _scoreChanger = scroreChanger;
            _scoreListener = scoreListener;
            
            // TODO TRY LOAD DATA OR...
            
            CurrentLevel = 0;
        }

        public int GetEarnedStarsAmount()
        {
            int amount = 0;
            
            foreach (var treshold in StarsTresholds)
            {
                if (Score >= treshold)
                    amount++;
            }

            return amount;
        }
        
        public void ClearTempData()
        {
            Coins = 0;
            PushedEnemies = 0;
            TotalEnemiesNumber = 0;
            _scoreChanger.ChangeScore(0);
            Debug.LogWarning("Статистика по уровню очищена.");
        }

        public void HandleSpawnedEnemies(int amount)
        {
            TotalEnemiesNumber += amount;
        }
        
        public void HandleKilledEnemy(int rewardValue)
        {
            OnEnemyPushed?.Invoke();
            PushedEnemies++;
            _scoreChanger.IncreaseScore(rewardValue);
        }

        public void HandleClearedStage()
        {
            OnStageCleared?.Invoke();
        }
        
        public void HandlePickedCoin()
        {
            OnCoinPicked?.Invoke();
            _wallet.Reward(1);
            Coins++;
        }

        public void HandlePickedPowerUp()
        {
            OnPowerUpPicked?.Invoke();
        }
        
    }
}